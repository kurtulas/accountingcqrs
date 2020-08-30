using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Business.Accounts
{
    public class AccountId : Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {
        }


    }
}
