using NSuperTest;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Transaction.Tests
{
    public class TransactionApiTests
    {
        TestClient client;
        public TransactionApiTests()
        {
            client = new TestClient("TrasnactionServer");
        }

        [Fact]
        public async Task ShouldGetTransactions()
        {
            var request = new GetRequest("/api/Transaction");

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }

     
        [Fact]
        public async Task ShouldSaveTransaction()
        {
            var request = new PostRequest("/api/Transaction", new
            {
                accountid = "test",
                customerid = "customer",
                amount = 0
            });

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }
      
    }
}
