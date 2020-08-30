using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Events;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Accounting.Domain.Business.Customers
{
    public class CustomerAggregate : AggregateRoot<CustomerAggregate, CustomerId>
    {
        private readonly CustomerAggregateState _customerState = new CustomerAggregateState();

        public CustomerAggregate(CustomerId id) : base(id)
        {
            Register(_customerState);
        }

        public IExecutionResult RegisterCustomer(string name, string surname, float initialCredit)
        { 
            Emit(new CustomerRegisteredEvent(new CustomerEntity(this.Id)
            {
                Name = name,
                Surname = surname
            }, initialCredit));

            return ExecutionResult.Success();

        }

        public void RegisterCustomerComplete(CustomerEntity entity, float initialCredits)
        {
            Emit(new CustomerRegisterCompletedEvent(entity, initialCredits));

        }


    }
}
