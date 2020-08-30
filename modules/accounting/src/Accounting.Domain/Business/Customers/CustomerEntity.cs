using Accounting.Domain.Business.Accounts;
using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Customers
{
    public class CustomerEntity : Entity<CustomerId>
    {
        public CustomerEntity(CustomerId id) : base(id)
        {
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public float Balance { get; set; }

        public virtual HashSet<AccountEntity> Accounts { get; set; }

    }
}
