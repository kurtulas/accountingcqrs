using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Events;
using Accounting.Domain.Business.Transactions;
using Accounting.Domain.Business.Transactions.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace Accounting.ReadModel
{
    public class TransactionReadModel : IReadModel,
        IAmReadModelFor<TransactionAggregate, TransactionId, TransactionRegisterCompletedEvent>

    {
        public virtual string AggregateId { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string AccountId { get; set; }
        public virtual float Amount { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<TransactionAggregate, TransactionId, TransactionRegisterCompletedEvent> domainEvent)
        {

            var _transaction = domainEvent.AggregateEvent.Entity;

            AggregateId = domainEvent.AggregateIdentity.ToString();
            CustomerId = domainEvent.AggregateEvent.Entity.CustomerId;
            AccountId = domainEvent.AggregateEvent.Entity.AccountId;
            Amount = domainEvent.AggregateEvent.Entity.Amount;

        }

        public TransactionEntity ToTransaction()
        {
            return new TransactionEntity(TransactionId.With(AggregateId))
            {
                Amount = Amount,
                AccountId = AccountId,
                CustomerId = CustomerId
            };
        }
    }

}