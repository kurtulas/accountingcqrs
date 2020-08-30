using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Business.Customers;
using Accounting.Domain.Business.Customers.Commands;
using Accounting.Service.ViewModels;
using EventFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.CommandServices
{
    public class CustomerCommandService : ICustomerCommandService
    {
        private readonly ICommandBus _commandBus;

        public CustomerCommandService(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }
     
        public async Task CreateCustomerAsync(string name, string surname, float initialCredit, CancellationToken ctx)
        {
            await _commandBus.PublishAsync(
               new RegisterCustomerCommand(name, surname, initialCredit),
               ctx);

        }
    }
}
