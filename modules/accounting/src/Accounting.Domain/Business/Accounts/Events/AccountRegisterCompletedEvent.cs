using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Accounts.Events
{
    public class AccountRegisterCompletedEvent : AggregateEvent<AccountAggregate, AccountId>
    {
        public AccountEntity Entity { get; }

        public float InitialCredit { get; set; }

         
        public AccountRegisterCompletedEvent(AccountEntity entity, float initialCredit)
        {
            Entity = entity;
            InitialCredit = initialCredit;
        }

    }
}
