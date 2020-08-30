using Accounting.Domain.Business.Customers;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Customers.Events
{
    public class CustomerRegisterCompletedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {

        public CustomerEntity Entity { get; }

        public float InitialCredit { get; set; }

        public CustomerRegisterCompletedEvent(CustomerEntity entity, float initialCredit)
        {
            Entity = entity;
            InitialCredit = initialCredit;
        }

    }

}

