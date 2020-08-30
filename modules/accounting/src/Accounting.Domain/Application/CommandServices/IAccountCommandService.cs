using Accounting.Domain.Business.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.CommandServices
{
    public interface IAccountCommandService
    {
        Task CreateAccountAsync(string customerId, string name, float initialCredit, CancellationToken ctx);
    }
}
