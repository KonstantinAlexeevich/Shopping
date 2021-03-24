using System.Collections.Generic;
using Domain.Foundation.Events;
using Domain.Foundation.Tactical;

namespace Domain.Foundation.EventSourcing
{
    public interface IEventsAggregate<TIdentity, in TEvent> : IAggregate<TIdentity>, IRecoverFrom<IEnumerable<TEvent>> 
        where TEvent : IEvent
    {
        
    }
}