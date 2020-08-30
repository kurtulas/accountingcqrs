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
    public class RegisterTransactionCompleteCommand : Command<TransactionAggregate, TransactionId, IExecutionResult>
    {
        public TransactionEntity Entity { get; set; }
        
        public RegisterTransactionCompleteCommand(TransactionEntity entity) : base(TransactionId.New)
        {
            Entity = entity;
        }
    }

    internal class RegisterTransactionCompleteCommandHandler : CommandHandler<TransactionAggregate, TransactionId, RegisterTransactionCompleteCommand>
    {
        public override Task ExecuteAsync(TransactionAggregate aggregate, RegisterTransactionCompleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.TransactionRegisterCompleted(command.Entity);
            return Task.CompletedTask;
        }
    }
}
