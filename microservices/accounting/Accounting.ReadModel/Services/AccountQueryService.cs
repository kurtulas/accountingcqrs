using Accounting.Domain.Application.QueryServices;
using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Customers;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.ReadModel.Services
{
    public class AccountQueryService : IAccountQueryService
    {
        private readonly IQueryProcessor _queryProcessor;

        public AccountQueryService(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<List<AccountEntity>> GetAccountsAsync(CancellationToken ctx)
        {

            var result = await _queryProcessor.ProcessAsync(
                new InMemoryQuery<AccountReadModel>(x => true), ctx);
                return result?.Select(x => new AccountEntity(new AccountId(x.AggregateId))
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    Balance = x.Balance
                }).ToList();

        }

        public async Task<List<AccountEntity>> GetAccountsByCustomerIdAsync(string customerId, CancellationToken ctx)
        {

            var result = await _queryProcessor.ProcessAsync(
                new InMemoryQuery<AccountReadModel>(x => x.CustomerId == customerId), ctx);
            return result?.Select(x => new AccountEntity(new AccountId(x.AggregateId))
            {
                CustomerId = x.CustomerId,
                Name = x.Name,
                Balance = x.Balance
            }).ToList();

        }

        public async Task<AccountEntity> GetAccountByIdAsync(AccountId customerId, CancellationToken ctx)
        {
            var result = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(customerId), ctx);
            return result?.ToAccount();

        }
    }
}


