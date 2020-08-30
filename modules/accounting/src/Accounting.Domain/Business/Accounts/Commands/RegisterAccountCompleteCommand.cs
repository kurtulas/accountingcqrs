using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace Accounting.Domain.Business.Accounts.Commands
{
    public class RegisterAccountCompleteCommand : Command<AccountAggregate, AccountId>
    {
        public AccountEntity Account { get; }
        public RegisterAccountCompleteCommand(AccountEntity account, float initialCredit) : base(AccountId.New)
        {
            Account = account;
            InitialCredit = initialCredit;
        }
        
        public float InitialCredit { get;}

    }

    internal class RegisterAccountCompleteCommandHandler : CommandHandler<AccountAggregate, AccountId, RegisterAccountCompleteCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, RegisterAccountCompleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.AccountRegisterComplete(command.Account);
            return Task.CompletedTask;
        }
    }
}
