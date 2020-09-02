using Accounting.Domain.Application.QueryServices;
using Accounting.Domain.Business.Accounts;
using Accounting.Domain.Business.Accounts.Events;
using Accounting.Domain.Business.Transactions.Commands;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.ReadStores;
using EventFlow.ReadStores.InMemory;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transaction.ReadModel.Services;
using Transaction.ReadModel;

using Infrastructure.RabbitMq;
using EventFlow.RabbitMQ;
using Transaction.ReadModel.EventHandlers;

namespace Transaction.ReadModel.Module
{
    public class TransactionReadModelModule : IModule
    {
        public void Register(IEventFlowOptions options)
        {

            options
                .AddDefaults(typeof(TransactionReadModelModule).Assembly)                
                .Configure(cfg => cfg.IsAsynchronousSubscribersEnabled = true)
                .SubscribeToRabbitMq(RabbitMqConfiguration.With(new Uri(@"amqp://guest:guest@localhost:5672")))
                .UseInMemoryReadStoreFor<TransactionServiceReadModel>()
                //.AddSubscribers(typeof(AllEventsSubscriber))
                //.AddAsynchronousSubscriber<AccountAggregate, AccountId,AccountRegisterCompletedEvent, AccountRegisterCompletedSubscriber>()      
                
                .RegisterServices(s => {                   
                    s.Register<ITransactionQueryService, TransactionQueryService>();
                    s.Register<IReadModelStore<TransactionServiceReadModel>,
                        InMemoryReadStore<TransactionServiceReadModel>>();
                    s.Register<ISubscribeSynchronousToAll, RabbitMqDomainEventSubscriber>();

                });
        }


      
    }
}
