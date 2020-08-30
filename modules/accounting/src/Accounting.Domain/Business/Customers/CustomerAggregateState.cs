using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Domain.Business.Customers.Events;
using EventFlow.Aggregates;
 

namespace Accounting.Domain.Business.Customers
{
    public class CustomerAggregateState : 
        AggregateState<CustomerAggregate, CustomerId, CustomerAggregateState>,
        IApply<CustomerRegisteredEvent>,
        IApply<CustomerRegisterCompletedEvent>
    {

        public void Apply(CustomerRegisteredEvent aggregateEvent)
        {
            // state change logic here 
        }

        public void Apply(CustomerRegisterCompletedEvent aggregateEvent)
        {
            
        }
    }
}
