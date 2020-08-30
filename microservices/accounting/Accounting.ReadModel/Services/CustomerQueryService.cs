using Accounting.Domain.Application.QueryServices;
using Accounting.Domain.Business.Customers;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.ReadModel.Services
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly IQueryProcessor _queryProcessor;

        public CustomerQueryService(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<List<CustomerEntity>> GetCustomersAsync(CancellationToken ctx)
        {
         
            var result = await _queryProcessor.ProcessAsync(
                new InMemoryQuery<CustomerReadModel>(x=> true), ctx);
            return result?.Select(x => new CustomerEntity(new CustomerId(x.AggregateId)) { 
                Name = x.Name,
                Surname = x.Surname
            }).ToList();

        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(CustomerId customerId, CancellationToken ctx)
        {
            var result = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<CustomerReadModel>(customerId), ctx);
            return result?.ToCustomer();

        }
    }
}
