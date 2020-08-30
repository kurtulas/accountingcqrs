using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;

namespace Accounting.Domain.Business.Transactions.Events
{
    public class TransactionRegisterCompletedEvent : AggregateEvent<TransactionAggregate, TransactionId>
    {
        public TransactionEntity Entity { get; }

        public TransactionRegisterCompletedEvent(TransactionEntity entity)
        {
            Entity = entity;
        }
    }
}
