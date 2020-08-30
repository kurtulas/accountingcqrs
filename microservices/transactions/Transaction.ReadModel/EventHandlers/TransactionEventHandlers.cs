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
        ISubscribeSynchronousTo<TransactionAggregate, TransactionId, TransactionRegisteredEvent>,
        ISubscribeSynchronousTo<TransactionAggregate, TransactionId, TransactionRegisterCompletedEvent>
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadModelStore<TransactionReadModel> _transactionReadModelStore;

        public TransactionEventHandlers(ICommandBus commandBus, IReadModelStore<TransactionReadModel> transactionReadModelStore)
        {
            _commandBus = commandBus;
            _transactionReadModelStore = transactionReadModelStore;
        }
   
        public async Task HandleAsync(IDomainEvent<TransactionAggregate, TransactionId, TransactionRegisteredEvent> domainEvent, CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new RegisterTransactionCompleteCommand(domainEvent.AggregateEvent.Transaction),cancellationToken);
        }

        public async Task HandleAsync(IDomainEvent<TransactionAggregate, TransactionId, TransactionRegisterCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            
        }


    }
}
