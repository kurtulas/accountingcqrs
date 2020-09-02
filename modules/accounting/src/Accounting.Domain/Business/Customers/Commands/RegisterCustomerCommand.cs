using Accounting.Domain.Business.Customers;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Business.Customers.Commands
{
    public class RegisterCustomerCommand : Command<CustomerAggregate, CustomerId, IExecutionResult>
    {
        public RegisterCustomerCommand(string name, string surname, float initialCredit) : base(CustomerId.New)
        {
            Name = name;
            Surname = surname;
            InitialCredit = initialCredit;
        }

        public string Name { get; }

        public string Surname { get; }

        public float InitialCredit { get; }
    }

    internal class RegisterCustomerCommandHandler : CommandHandler<CustomerAggregate, CustomerId, IExecutionResult, RegisterCustomerCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(CustomerAggregate aggregate,
            RegisterCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = aggregate.RegisterCustomer(command.Name, command.Surname, command.InitialCredit);
            return Task.FromResult(result);
        }
    }
}
