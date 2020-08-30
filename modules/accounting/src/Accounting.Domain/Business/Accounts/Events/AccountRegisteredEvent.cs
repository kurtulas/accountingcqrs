using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Aggregates;
using Accounting.Domain.Business.Accounts;

namespace Accounting.Domain.Business.Accounts.Events
{
    public class AccountRegisteredEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountEntity Account { get; set; }
        public float InitialCredit { get; set; }

        public AccountRegisteredEvent(AccountEntity entity, float initialCredit)
        {
            Account = entity;
            InitialCredit = initialCredit;
        }
    }
}
