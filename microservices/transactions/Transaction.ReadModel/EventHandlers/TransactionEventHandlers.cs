using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Events;
using Accounting.Domain.Business.Transactions;
using Accounting.Domain.Business.Transactions.Commands;
using Accounting.Domain.Business.Transactions.Events;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transaction.ReadModel.EventHandlers
{
    public class TransactionEventHandlers :
        ISubscribeAsynchronousTo<AccountAggregate, AccountId, AccountRegisterCompletedEvent>,
        ISubscribeSynchronousTo<TransactionAggregate, TransactionId, TransactionRegisteredEvent> 
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadModelStore<TransactionServiceReadModel> _transactionReadModelStore;

        public TransactionEventHandlers(ICommandBus commandBus, IReadModelStore<TransactionServiceReadModel> transactionReadModelStore)
        {
            _commandBus = commandBus;
            _transactionReadModelStore = transactionReadModelStore;
        }
   
        public async Task HandleAsync(IDomainEvent<TransactionAggregate, TransactionId, TransactionRegisteredEvent> domainEvent, CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new RegisterTransactionCompleteCommand(domainEvent.AggregateEvent.Transaction),cancellationToken);
        }
 
        public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, AccountRegisterCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new RegisterTransactionCommand(
                domainEvent.AggregateEvent.Entity.CustomerId,
                domainEvent.AggregateEvent.Entity.Id.Value,
                domainEvent.AggregateEvent.InitialCredit
                ), cancellationToken);
        }
    }
}
