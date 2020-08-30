using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Core;
using EventFlow.Entities;

namespace Accounting.Domain.Business.Transactions
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value)
        {
        }
    }
}
