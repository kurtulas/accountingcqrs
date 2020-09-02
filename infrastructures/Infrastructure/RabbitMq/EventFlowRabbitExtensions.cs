using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.RabbitMQ;
using EventFlow.RabbitMQ.Integrations;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Infrastructure.RabbitMq
{
    public static class EventFlowRabbitExtensions
    {

        public static IEventFlowOptions SubscribeToRabbitMq(
            this IEventFlowOptions eventFlowOptions,
            IRabbitMqConfiguration configuration)
        {
            var registrations = eventFlowOptions.RegisterServices(sr =>
            {
                sr.Register<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>(Lifetime.Singleton);
                sr.Register<IRabbitMqMessageFactory, RabbitMqMessageFactory>(Lifetime.Singleton);
                sr.Register<IRabbitMqSubscriber, RabbitMqSubscriber>(Lifetime.Singleton);
                sr.Register<IRabbitMqRetryStrategy, RabbitMqRetryStrategy>(Lifetime.Singleton);

                sr.Register(rc => configuration, Lifetime.Singleton);
                sr.Register<ISubscribeSynchronousToAll, RabbitMqDomainEventSubscriber>();
            });

            return registrations;
        }

        public static void Listen(IList<IDomainEvent> domainEvents, IDomainEventPublisher domainEventPublisher)
        {

            domainEventPublisher.PublishAsync(
               (IReadOnlyCollection<IDomainEvent>)domainEvents,
                CancellationToken.None).Wait();

        }

    }
}
