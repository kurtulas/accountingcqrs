using EventFlow;
using EventFlow.Configuration;
using EventFlow.RabbitMQ;
using EventFlow.RabbitMQ.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Transaction.Service
{
    public static class SubscribeToRabbitMqExtensions
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
            });

            return registrations;
        }

    }
}
