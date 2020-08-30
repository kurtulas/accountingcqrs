using Accounting.Domain.Business.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.CommandServices
{
    public interface ITransactionCommandService
    {
        Task CreateTransactionAsync(string customerId, string accountId, float amount, CancellationToken ctx);
    }
}
