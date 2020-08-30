using Accounting.Domain.Business.Customers;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Customers.Events
{
    public class CustomerRegisteredEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public CustomerEntity Customer { get; set; }
        public float InitialCredit { get; set; }
        public CustomerRegisteredEvent(CustomerEntity entity, float initialCredit)
        {
            Customer = entity;
            InitialCredit = initialCredit;
        }
    }
}
