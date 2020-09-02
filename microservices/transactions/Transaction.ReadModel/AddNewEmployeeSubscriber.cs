using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Events;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transaction.ReadModel
{
    public class AccountRegisterCompletedSubscriber 
        : ISubscribeAsynchronousTo<AccountAggregate, AccountId, AccountRegisterCompletedEvent>
    {

        //protected readonly ICommandBus CommandBus;
        public AccountRegisterCompletedSubscriber()
        {
            
        }

        public Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountRegisterCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
