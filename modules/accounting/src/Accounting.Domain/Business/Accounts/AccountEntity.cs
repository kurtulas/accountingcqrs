using Accounting.Domain.Business.Transactions;
using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Accounts
{
    public class AccountEntity : Entity<AccountId>
    {
        public AccountEntity(AccountId id) : base(id)
        {
            
        }

        public string CustomerId { get; set; }
        public string Name { get; set; }
        public float Balance { get; set; }

        public virtual HashSet<TransactionEntity> Transactions { get; set; }

    }
}
