using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Business.Transactions.Commands;
using EventFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Transaction.Service.CommandServices
{
    public class TransactionCommandService : ITransactionCommandService
    {

        private readonly ICommandBus _commandBus;

        public TransactionCommandService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task CreateTransactionAsync(string customerId , string accountId, float amount, CancellationToken ctx)
        {
            await _commandBus.PublishAsync(
               new RegisterTransactionCommand(customerId, accountId, amount),
               ctx);

        }
    }
}
