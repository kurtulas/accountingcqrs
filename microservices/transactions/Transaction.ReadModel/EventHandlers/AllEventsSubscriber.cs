using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transaction.ReadModel.EventHandlers
{
    public class AllEventsSubscriber : ISubscribeSynchronousToAll
    {

        protected readonly ICommandBus CommandBus;
        public AllEventsSubscriber(ICommandBus commandBus)
        {
            CommandBus = commandBus;
        }


        public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents,
            CancellationToken cancellationToken)
        {
                // All events subscriber
        }
    }
}
