using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Foundation.Core;
using Domain.Foundation.Events;
using Domain.Foundation.EventSourcing;
using Domain.Foundation.Storage;
using EventStore.Client;

namespace Domain.Foundation.EventStore
{
    public class EventsAggregateStore<TAggregate, TIdentity, TEvent> : AggregateStore<TAggregate, TIdentity, IEnumerable<TEvent>>
        where TEvent : IEvent
        where TAggregate : IEventsAggregate<TIdentity, TEvent>
    {
        private readonly EventStoreClient _client;
        private readonly IEventMetadataProvider _eventMetadataProvider;

        public EventsAggregateStore(
            IEventMetadataProvider eventMetadataProvider,
            EventStoreClient client,
            IAggregateConstructor aggregateConstructor
        ) : base(aggregateConstructor)
        {
            _eventMetadataProvider = eventMetadataProvider;
            _client = client;
        }

        protected override async Task Load(TAggregate aggregate)
        {
            var stream = GetStreamName<TAggregate>(aggregate.GetId());

            var read = _client.ReadStreamAsync(Direction.Forwards, stream, StreamPosition.Start);
            var resolvedEvents = await read.ToArrayAsync();
            var events = resolvedEvents.Select(Deserialize<TEvent>);

            await aggregate.Restore(events).ConfigureAwait(false);
        }

        private T Deserialize<T>(ResolvedEvent resolvedEvent)
        {
            var dataType = _eventMetadataProvider.GetType(resolvedEvent.Event.EventType);
            var data = JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, dataType);
            return (T) data;
        }

        public override async Task Store(TAggregate aggregate)
        {
            var stream = GetStreamName<TAggregate>(aggregate.GetId());
            var changes = aggregate.Changes.ToArray();
            var events = changes.Select(x => CreateEventData(x));

            var resultTask = aggregate.Version < 0
                ? _client.AppendToStreamAsync(stream, StreamState.NoStream, events)
                : _client.AppendToStreamAsync(stream, StreamRevision.FromInt64(aggregate.Version), events);
            
            var result = await resultTask.ConfigureAwait(false);

            aggregate.ClearChanges();
        }
        
        EventData CreateEventData(object e)
        {
            return new EventData(
                Uuid.NewUuid(),
                _eventMetadataProvider.GetTypeName(e.GetType()),
                JsonSerializer.SerializeToUtf8Bytes(e)
            );
        }
        private static string GetStreamName<T>(string entityId)
        {
            return $"{typeof(T).Name}-{entityId}";
        }
    }
}