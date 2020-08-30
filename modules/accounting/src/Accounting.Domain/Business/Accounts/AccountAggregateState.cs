
using Accounting.Domain.Business.Accounts.Events;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Accounts
{
    public class AccountAggregateState : AggregateState<AccountAggregate, AccountId, AccountAggregateState>,
        IApply<AccountRegisteredEvent>,
        IApply<AccountRegisterCompletedEvent>

    {
        public void Apply(AccountRegisterCompletedEvent aggregateEvent)
        {
            //throw new NotImplementedException();
        }

        public void Apply(AccountRegisteredEvent aggregateEvent)
        {
            //throw new NotImplementedException();
        }
    }
}
