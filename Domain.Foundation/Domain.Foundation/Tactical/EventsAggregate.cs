using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Foundation.Events;
using Domain.Foundation.EventSourcing;

namespace Domain.Foundation.Tactical
{
    public abstract class EventsAggregate<TIdentity, TEvent> : IEventsAggregate<TIdentity, TEvent> 
        where TEvent : IEvent
    {
        protected void Apply(TEvent evt) {
            _changes.Add(evt);
            When(evt);
        }

        public Task Restore(IEnumerable<TEvent> events)
        {
            foreach (var @event in events) {
                _existing.Add(@event);
                When(@event);
                Version++;
            }
            
            return Task.CompletedTask;
        }

        protected abstract void When(TEvent evt);

        public IReadOnlyCollection<TEvent> Changes => _changes.AsReadOnly();
        protected IReadOnlyCollection<TEvent> Existing => _existing.AsReadOnly();
        public void ClearChanges() => _changes.Clear();
        public abstract string GetId();
        public int Version { get; private set; } = -1;

        readonly List<TEvent> _existing = new();
        readonly List<TEvent> _changes = new();
        public Task<IEnumerable<TEvent>> Store() => Task.FromResult(Changes.AsEnumerable());
    }
}