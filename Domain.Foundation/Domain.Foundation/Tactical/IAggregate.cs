using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Foundation.Tactical
{
    public interface IAggregate: IRecoverFrom<Unit>
    {
    }
    
    public interface IAggregate<TIdentity> : IAggregate 
    {
    }
}