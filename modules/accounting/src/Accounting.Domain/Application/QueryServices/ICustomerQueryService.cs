using Accounting.Domain.Business.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Domain.Application.QueryServices
{
    public interface ICustomerQueryService
    {
        Task<CustomerEntity> GetCustomerByIdAsync(CustomerId customerId, CancellationToken ctx);
        Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken ctx);

    }
}
