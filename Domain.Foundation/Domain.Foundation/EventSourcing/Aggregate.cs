using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Foundation.Events;

namespace Domain.Foundation.EventSourcing
{
    public abstract class Aggregate<TIdentity, TEvent> : IEventsAggregate<TIdentity, TEvent> where TEvent : IEvent
    {
        protected void Apply(TEvent evt) {
            _changes.Add(evt);
            When(evt);
        }

        public Task LoadAsync(IEnumerable<TEvent> events)
        {
            foreach (var @event in events) {
                _existing.Add(@event);
                When(@event);
                Version++;
            }
            
            return Task.CompletedTask;
        }

        protected abstract void When(object evt);

        public IReadOnlyCollection<object> Changes => _changes.AsReadOnly();

        protected IReadOnlyCollection<object> Existing => _existing.AsReadOnly();

        public void ClearChanges() => _changes.Clear();

        public abstract string GetId();
        public int Version { get; private set; } = -1;

        readonly List<object> _existing = new();
        readonly List<object> _changes = new();
    }
}