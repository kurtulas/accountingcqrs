using Accounting;
using Transaction;
using PublicGateway;
using Microsoft.Extensions.Configuration;
using NSuperTest.Registration;
using NSuperTest.Registration.NetCoreServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gateway.Tests
{
    public class ServerRegistrar : IRegisterServers
    {
        public void Register(ServerRegistry reg)
        {
            var accountingServiceconfig = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            reg.RegisterNetCoreServer<Accounting.Service.Startup>("AccountingServer", accountingServiceconfig);

            var trasnactionServiceConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            reg.RegisterNetCoreServer<Transaction.Service.Startup>("TransactionServer", trasnactionServiceConfig);

            var gatewayServiceConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddJsonFile("ocelot.json");
            reg.RegisterNetCoreServer<PublicGateway.Startup>("GatewayServer", gatewayServiceConfig);
        }
    }
}
