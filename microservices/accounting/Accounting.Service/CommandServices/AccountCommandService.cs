using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Commands;
using EventFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.CommandServices
{
    public class AccountCommandService : IAccountCommandService
    {
        private readonly ICommandBus _commandBus;

        public AccountCommandService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task CreateAccountAsync(string customerId, string name, float initialCredit, CancellationToken ctx)
        {
            await _commandBus.PublishAsync(
              new RegisterAccountCommand(customerId, name, initialCredit),
              ctx);
        }
    }
}
