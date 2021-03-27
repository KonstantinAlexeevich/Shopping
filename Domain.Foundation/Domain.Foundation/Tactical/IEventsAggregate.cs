using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Foundation.Events;
using Domain.Foundation.Tactical;

namespace Domain.Foundation.EventSourcing
{
    public interface IEventsAggregate<TIdentity, TEvent> : 
        IAggregate<TIdentity>, 
        IRestoredFrom<IEnumerable<TEvent>> ,
        IStoredTo<IEnumerable<TEvent>> where TEvent : IEvent
    {
        IReadOnlyCollection<TEvent> Changes { get; }
        int Version { get; }
        void ClearChanges();
        string GetId();
    }
}