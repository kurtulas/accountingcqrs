using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.QueryServices
{
    public interface IAccountQueryService
    {
        Task<List<AccountEntity>> GetAccountsByCustomerIdAsync(string customerId, CancellationToken ctx);
        Task<AccountEntity> GetAccountByIdAsync(AccountId customerId, CancellationToken ctx);
        Task<List<AccountEntity>> GetAccountsAsync(CancellationToken ctx);

    }
}
