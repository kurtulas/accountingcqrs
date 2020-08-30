using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Accounting.Domain.Business.Accounts.Commands
{
    public class RegisterAccountCommand : Command<AccountAggregate, AccountId>
    {
        public RegisterAccountCommand(string customerId ,string name , float initialCredit) : base(AccountId.New)
        {
            CustomerId = customerId;
            Name = name;
            InitialCredit = initialCredit;
        }

        public string CustomerId { get; }
        public string Name { get; }
        public float InitialCredit { get;}

    }

    internal class RegisterAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, RegisterAccountCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate, RegisterAccountCommand command, CancellationToken cancellationToken)
        {            
            var result = aggregate.RegisterAccount(command.CustomerId, command.Name, command.InitialCredit);
            return Task.FromResult(result);
        }
    }
}
