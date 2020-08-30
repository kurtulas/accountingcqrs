using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Domain.Business.Transactions.Events;
using EventFlow.Aggregates;

namespace Accounting.Domain.Business.Transactions
{
    public class TransactionAggregateState :
        AggregateState<TransactionAggregate, TransactionId, TransactionAggregateState>,
        IApply<TransactionRegisteredEvent>,
        IApply<TransactionRegisterCompletedEvent>
    {
        public void Apply(TransactionRegisteredEvent aggregateEvent)
        {
            // state 
        }

        public void Apply(TransactionRegisterCompletedEvent aggregateEvent)
        {
            
        }
    }
}
