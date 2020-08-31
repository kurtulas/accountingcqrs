using Microsoft.Extensions.Configuration;
using NSuperTest.Registration;
using NSuperTest.Registration.NetCoreServer;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Service;

namespace Transaction.Tests
{
    public class ServerRegistrar : IRegisterServers
    {
        public void Register(ServerRegistry reg)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            reg.RegisterNetCoreServer<Startup>("TrasnactionServer", config);
        }
    }
}
