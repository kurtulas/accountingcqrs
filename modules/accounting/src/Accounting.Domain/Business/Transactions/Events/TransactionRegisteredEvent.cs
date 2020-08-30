using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;
using Accounting.Domain.Business.Transactions;

namespace Accounting.Domain.Business.Transactions.Events
{
    public class TransactionRegisteredEvent : AggregateEvent<TransactionAggregate, TransactionId>
    {
        public TransactionEntity Transaction { get; set; }
        public TransactionRegisteredEvent(TransactionEntity entity)
        {
            Transaction = entity;
        }
    }
}
