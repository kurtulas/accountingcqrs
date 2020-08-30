using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Customers
{
    public class CustomerId : Identity<CustomerId>
    {
        public CustomerId(string value) : base(value)
        {
        }
    }
}
