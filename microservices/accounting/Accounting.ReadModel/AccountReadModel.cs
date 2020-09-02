using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Events;
using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace Accounting.ReadModel
{
    public class AccountReadModel : IReadModel,
        IAmReadModelFor<AccountAggregate, AccountId, AccountRegisterCompletedEvent>
    {
        public virtual string AggregateId { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual float Balance { get; set; }

        public void Apply(IReadModelContext context, 
            IDomainEvent<AccountAggregate, AccountId, AccountRegisterCompletedEvent> domainEvent)
        {
            var _account = domainEvent.AggregateEvent.Entity;

            AggregateId = domainEvent.AggregateIdentity.ToString();
            CustomerId = _account.CustomerId;
            Name = _account.Name;
            Balance = _account.Balance;
        }

        public AccountEntity ToAccount()
        {
            return new AccountEntity(AccountId.With(AggregateId))
            {
                Name = Name,
                Balance = Balance
            };
        }

    }
}
