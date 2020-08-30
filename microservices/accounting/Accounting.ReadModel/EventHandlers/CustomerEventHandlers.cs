using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Events;
using EventFlow;
using EventFlow.ReadStores;
using EventFlow.Subscribers;
using EventFlow.Aggregates;
using Accounting.Domain.Business.Customers.Commands;
using Accounting.Domain.Business.Accounts.Commands;

namespace Accounting.ReadModel.EventHandlers
{
    public class CustomerEventHandlers :
        ISubscribeSynchronousTo<CustomerAggregate, CustomerId, CustomerRegisteredEvent>,
        ISubscribeSynchronousTo<CustomerAggregate, CustomerId, CustomerRegisterCompletedEvent>
    {

        private readonly ICommandBus _commandBus;
        private readonly IReadModelStore<CustomerReadModel> _customerReadModelStore;

        public CustomerEventHandlers(ICommandBus commandBus,
          IReadModelStore<CustomerReadModel> customerReadModelStore)
        {
            _commandBus = commandBus;
            _customerReadModelStore = customerReadModelStore;
        }

        public async Task HandleAsync(IDomainEvent<CustomerAggregate, CustomerId, CustomerRegisteredEvent> domainEvent,
            CancellationToken cancellationToken)
        {
            await _commandBus.PublishAsync(new RegisterCustomerCompleteCommand(domainEvent.AggregateEvent.Customer, 
                domainEvent.AggregateEvent.InitialCredit),
                cancellationToken);
        }

        public async Task HandleAsync(IDomainEvent<CustomerAggregate, CustomerId, CustomerRegisterCompletedEvent> domainEvent, CancellationToken cancellationToken)
        {
            if (domainEvent.AggregateEvent.InitialCredit > 0)
            {
                await _commandBus.PublishAsync(new RegisterAccountCommand(domainEvent.AggregateIdentity.Value, "Initial Account", domainEvent.AggregateEvent.InitialCredit),
               cancellationToken);
            }
            
        }
    }
}
