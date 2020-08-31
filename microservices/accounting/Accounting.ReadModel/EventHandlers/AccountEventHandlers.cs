using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Commands;
using Accounting.Domain.Business.Accounts.Events;
using Accounting.Domain.Business.Transactions;
using Accounting.Domain.Business.Transactions.Commands;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.ReadModel.EventHandlers
{
    public class AccountEventHandlers :
        ISubscribeSynchronousTo<AccountAggregate, AccountId, AccountRegisteredEvent>,
        ISubscribeSynchronousTo<AccountAggregate, AccountId, AccountRegisterCompletedEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadModelStore<AccountReadModel> _accountReadModelStore;

        public AccountEventHandlers(ICommandBus commandBus,
          IReadModelStore<AccountReadModel> accountReadModelStore)
        {
            _commandBus = commandBus;
            _accountReadModelStore = accountReadModelStore;
        }

        public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountRegisteredEvent> domainEvent,
           CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new RegisterAccountCompleteCommand(domainEvent.AggregateEvent.Account, domainEvent.AggregateEvent.InitialCredit),
                cancellationToken);
        }

        public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountRegisterCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent.AggregateEvent.InitialCredit > 0)
            {
                await _commandBus.PublishAsync(new RegisterTransactionCommand(domainEvent.AggregateIdentity.Value,
                    domainEvent.AggregateEvent.Entity.CustomerId,
                    domainEvent.AggregateEvent.InitialCredit
                ), cancellationToken);
            }
            
        }
    }
}
