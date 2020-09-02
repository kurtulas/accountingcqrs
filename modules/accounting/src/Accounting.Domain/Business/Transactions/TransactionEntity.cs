using System;
using System.Collections.Generic;
using System.Text;
using EventFlow;
using EventFlow.Entities;

namespace Accounting.Domain.Business.Transactions
{
    public class TransactionEntity : Entity<TransactionId>
    {
        public TransactionEntity(TransactionId id) : base(id)
        {
        }

        public string CustomerId { get; set; }
        public string AccountId { get; set; }
        public float Amount { get; set; }

    }
}
