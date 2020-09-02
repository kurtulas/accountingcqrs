using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMq
{
    public interface IRabbitMqSubscriber
    {
        Task SubscribeAsync(string exchange, string queue, 
            Action<IList<IDomainEvent>, IDomainEventPublisher> action, 
            IDomainEventPublisher domainEventPublisher, 
            CancellationToken cancellationToken);
    }
}
