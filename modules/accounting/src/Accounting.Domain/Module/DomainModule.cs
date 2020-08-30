using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Module
{
    public class DomainModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(DomainModule).Assembly);
        }
    }
}
