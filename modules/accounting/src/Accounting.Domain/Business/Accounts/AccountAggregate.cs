using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Domain.Business.Accounts.Events;

namespace Accounting.Domain.Business.Accounts
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        private readonly AccountAggregateState accountAggregateState = new AccountAggregateState();
        public AccountAggregate(AccountId id) : base(id)
        {
            Register(accountAggregateState);
        }

        public IExecutionResult RegisterAccount(string customerId , string name, float initialCredit)
        {
            Emit(new AccountRegisteredEvent(new AccountEntity(this.Id)
            {
                CustomerId = customerId,
                Name = name,
                Balance = initialCredit
            }, initialCredit));

            return ExecutionResult.Success();
        }

        public void AccountRegisterComplete(AccountEntity entity, float initialCredits)
        {
            Emit(new AccountRegisterCompletedEvent(entity, initialCredits));

        }
    }
}
