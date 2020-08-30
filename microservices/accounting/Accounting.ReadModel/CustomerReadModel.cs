using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace Accounting.ReadModel
{
    public class CustomerReadModel : IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, CustomerRegisterCompletedEvent>

    {
        public virtual string AggregateId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
       

        public void Apply(IReadModelContext context,
            IDomainEvent<CustomerAggregate, CustomerId, CustomerRegisterCompletedEvent> domainEvent)
        {

            var _customer = domainEvent.AggregateEvent.Entity;

            AggregateId = domainEvent.AggregateIdentity.ToString();
            Name = _customer.Name;
            Surname = _customer.Surname;
            
        }

        public CustomerEntity ToCustomer()
        {
            return new CustomerEntity(CustomerId.With(AggregateId))
            {
                Name = Name,
                Surname = Surname                
            };
        }
    }

}