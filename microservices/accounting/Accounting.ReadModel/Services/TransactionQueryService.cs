using Accounting.Domain.Application.QueryServices;
using Accounting.Domain.Business.Transactions;
using Accounting.ReadModel;
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
    public class TransactionQueryService : ITransactionQueryService
    {
        private readonly IQueryProcessor _queryProcessor;

        public TransactionQueryService(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<List<TransactionEntity>> GetTransactionsAsync(CancellationToken ctx)
        {

            var result = await _queryProcessor.ProcessAsync(
                new InMemoryQuery<TransactionReadModel>(x => true), ctx);
            return result?.Select(x => new TransactionEntity(new TransactionId(x.AggregateId))
            {
                AccountId = x.AccountId,
                CustomerId = x.CustomerId,
                Amount = x.Amount
            }).ToList();

        }

        public async Task<TransactionEntity> GetTransactionByIdAsync(TransactionId customerId, CancellationToken ctx)
        {
            var result = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<TransactionReadModel>(customerId), ctx);
            return result?.ToTransaction();

        }

        public async Task<List<TransactionEntity>> GetTransactionsByAccountIdAsync(string accountId, CancellationToken ctx)
        {

            var result = await _queryProcessor.ProcessAsync(
                new InMemoryQuery<TransactionReadModel>(x => x.AccountId == accountId), ctx);
            return result?.Select(x => new TransactionEntity(new TransactionId(x.AggregateId))
            {
                CustomerId = x.CustomerId,
                AccountId = x.AccountId,
                Amount = x.Amount
            }).ToList();

        }

    }
}
