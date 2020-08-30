using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Core;

namespace Accounting.Domain.Business.Transactions.Commands
{
    public class RegisterTransactionCommand : Command<TransactionAggregate, TransactionId, IExecutionResult>
    {
        public string AccountId { get; }

        public string CustomerId { get; }
        public float Amount { get;  }

        public RegisterTransactionCommand(string customerId, string accountId, float amount) : base(TransactionId.New)
        {
            AccountId = accountId;
            CustomerId = customerId;
            Amount = amount;
        }
    }

    internal class RegisterTransactionCommandHandler : CommandHandler<TransactionAggregate, TransactionId, IExecutionResult, RegisterTransactionCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(TransactionAggregate aggregate,
            RegisterTransactionCommand command, CancellationToken cancellationToken)
        {
            var result = aggregate.RegisterTransaction(command.CustomerId, command.AccountId, command.Amount);
            return Task.FromResult(result);
        }
    }
}
