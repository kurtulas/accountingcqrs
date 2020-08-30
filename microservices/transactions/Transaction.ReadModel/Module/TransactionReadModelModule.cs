using Accounting.Domain.Application.QueryServices;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.ReadModel.Services;

namespace Transaction.ReadModel.Module
{
    public class TransactionReadModelModule : IModule
    {
        public void Register(IEventFlowOptions options)
        {

            options
                .AddDefaults(typeof(TransactionReadModelModule).Assembly)
                .UseInMemoryReadStoreFor<TransactionReadModel>()
                .RegisterServices(s => {

                    s.Register<ITransactionQueryService, TransactionQueryService>();
                    s.Register<IReadModelStore<TransactionReadModel>, InMemoryReadStore<TransactionReadModel>>();
                });
        }
    }
}
