using Accounting.Domain.Application.QueryServices;
using Accounting.ReadModel.Services;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.ReadModel.Module
{
    public class AccountingReadModelModule : IModule
    {
        public void Register(IEventFlowOptions options)
        {

            options
                .AddDefaults(typeof(AccountingReadModelModule).Assembly)
                 .Configure(cfg => cfg.IsAsynchronousSubscribersEnabled = true)
                 
                .UseInMemoryReadStoreFor<CustomerReadModel>()
                .UseInMemoryReadStoreFor<AccountReadModel>()                
                .RegisterServices(s => {

                    s.Register<ICustomerQueryService, CustomerQueryService>();
                    s.Register<IAccountQueryService, AccountQueryService>();                    
                    s.Register<IReadModelStore<AccountReadModel>, InMemoryReadStore<AccountReadModel>>();
                    s.Register<IReadModelStore<CustomerReadModel>, InMemoryReadStore<CustomerReadModel>>();                                        
                });
        }
    }
}
