using Accounting.Domain.Business.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.CommandServices
{
    public interface ICustomerCommandService
    {
        Task CreateCustomerAsync(string name, string surname, float initialCredit, CancellationToken ctx);
    }
}
