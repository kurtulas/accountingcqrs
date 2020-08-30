using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Business.Customers.Commands
{
    public class RegisterCustomerCompleteCommand : Command<CustomerAggregate, CustomerId, IExecutionResult>
    {
        public CustomerEntity Customer { get; set; }

        public float InitialCredit { get; set; }
        public RegisterCustomerCompleteCommand(CustomerEntity entity , float initialCredit) : base(entity.Id) {
            Customer = entity;
            InitialCredit = initialCredit;
        }
 
    }

    internal class RegisterCustomerCompleteCommandHandler : CommandHandler<CustomerAggregate, CustomerId,RegisterCustomerCompleteCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, RegisterCustomerCompleteCommand command, CancellationToken cancellationToken)
        {
            aggregate.RegisterCustomerComplete(command.Customer, command.InitialCredit);
            return Task.CompletedTask;

        }
    }
}
