using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.QueryServices
{
    public interface ITransactionQueryService
    {
        Task<TransactionEntity> GetTransactionByIdAsync(TransactionId transactionId, CancellationToken ctx);
        Task<List<TransactionEntity>> GetTransactionsAsync(CancellationToken ctx);

        Task<List<TransactionEntity>> GetTransactionsByAccountIdAsync(string accountId, CancellationToken ctx);
    }
}
