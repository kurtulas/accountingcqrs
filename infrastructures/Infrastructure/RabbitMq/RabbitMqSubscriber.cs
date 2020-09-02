using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.Core;
using EventFlow.EventStores;
using EventFlow.Extensions;
using EventFlow.Logs;
using EventFlow.RabbitMQ;
using EventFlow.RabbitMQ.Integrations;
using EventFlow.Subscribers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Infrastructure.RabbitMq
{
    public class RabbitMqSubscriber : IDisposable, IRabbitMqSubscriber
    {
        private readonly ILog _log;
        private readonly IRabbitMqConnectionFactory _connectionFactory;
        private readonly IRabbitMqConfiguration _configuration;        
        private readonly AsyncLock _asyncLock = new AsyncLock();
        private readonly Dictionary<Uri, IRabbitConnection> _connections = new Dictionary<Uri, IRabbitConnection>();
        private readonly IRabbitMqMessageFactory _rabbitMqMessageFactory;
        private readonly IEventJsonSerializer _eventJsonSerializer;
        private readonly IDispatchToEventSubscribers _dispatchToEventSubscribers;


        public RabbitMqSubscriber(
            ILog log,
            IRabbitMqConnectionFactory connectionFactory,
            IRabbitMqConfiguration configuration,            
            IRabbitMqMessageFactory rabbitMqMessageFactory,
            IEventJsonSerializer eventJsonSerializer,
            IDispatchToEventSubscribers dispatchToEventSubscribers
           )
        {
            _log = log;
            _connectionFactory = connectionFactory;
            _configuration = configuration;            
            _rabbitMqMessageFactory = rabbitMqMessageFactory;
            _eventJsonSerializer = eventJsonSerializer;
            _dispatchToEventSubscribers = dispatchToEventSubscribers;
        }

        public async Task SubscribeAsync(string exchange, string queue, 
            Action<IList<IDomainEvent>, IDomainEventPublisher> action,
            IDomainEventPublisher domainEventPublisher, CancellationToken cancellationToken)
        {
            Uri uri = _configuration.Uri;
            IRabbitConnection rabbitConnection = null;

            try
            {
                rabbitConnection = await GetRabbitMqConnectionAsync(uri, cancellationToken).ConfigureAwait(false);

                await rabbitConnection.WithModelAsync(model => {
                    model.ExchangeDeclare(exchange, ExchangeType.Fanout);
                    model.QueueDeclare(queue, false, false, true, null);
                    model.QueueBind(queue, exchange, "");

                    var consume = new EventingBasicConsumer(model);
                    consume.Received += (obj, @event) => {
                        var msg = CreateRabbitMqMessage(@event);
                        var domainEvent = _eventJsonSerializer.Deserialize(msg.Message, new Metadata(msg.Headers));

                       _dispatchToEventSubscribers.DispatchToAsynchronousSubscribersAsync(domainEvent, cancellationToken);
                    };
                    model.BasicConsume(queue, false, consume);
                    return Task.CompletedTask;
                }, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (rabbitConnection != null)
                {
                    using (await _asyncLock.WaitAsync(CancellationToken.None).ConfigureAwait(false))
                    {
                        rabbitConnection.Dispose();
                        _connections.Remove(uri);
                    }
                }

                _log.Error(e, "Failed to subscribe to RabbitMQ");
                throw;
            }
        }

        private async Task<IRabbitConnection> GetRabbitMqConnectionAsync(Uri uri, CancellationToken cancellationToken)
        {
            using (await _asyncLock.WaitAsync(cancellationToken).ConfigureAwait(false))
            {
                IRabbitConnection rabbitConnection;

                if (_connections.TryGetValue(uri, out rabbitConnection))
                {
                    return rabbitConnection;
                }

                rabbitConnection = await _connectionFactory.CreateConnectionAsync(uri, cancellationToken).ConfigureAwait(false);

                _connections.Add(uri, rabbitConnection);

                return rabbitConnection;
            }
        }
 
        private static RabbitMqMessage CreateRabbitMqMessage(BasicDeliverEventArgs basicDeliverEventArgs)
        {
            var headers = basicDeliverEventArgs.BasicProperties.Headers.ToDictionary(kv => kv.Key,
                kv => Encoding.UTF8.GetString((byte[])kv.Value));
            var message = Encoding.UTF8.GetString(basicDeliverEventArgs.Body);

            return new RabbitMqMessage(
                message,
                headers,
                new Exchange(basicDeliverEventArgs.Exchange),
                new RoutingKey(basicDeliverEventArgs.RoutingKey),
                new MessageId(basicDeliverEventArgs.BasicProperties.MessageId));
        }

        public void Dispose()
        {
            foreach (var rabbitConnection in _connections.Values)
            {
                rabbitConnection.Dispose();
            }
            _connections.Clear();
        }
    }
}
