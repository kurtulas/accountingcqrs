using NSuperTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Gateway.Tests
{
    public class GatewayApiTests
    {
        TestClient client;
        public GatewayApiTests()
        {
            client = new TestClient("GatewayServer");
        }

        [Fact]
        public async Task ShouldGetTransactions()
        {
            var request = new GetRequest("/api/transaction/Transaction");

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }

        [Fact]
        public async Task ShouldGetCustomers()
        {
            var request = new GetRequest("/api/accounting/Customer");

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }

    }
}
