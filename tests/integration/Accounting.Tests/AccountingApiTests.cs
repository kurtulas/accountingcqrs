using FluentAssertions;
using NSuperTest;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;

namespace Accounting.Tests
{

    public class AccountingApiTests
    {
        TestClient client;
        public AccountingApiTests()
        {
            client = new TestClient("AccountingServer");
        }

        [Fact]
        public async Task ShouldGetAccounts()
        {
            var request = new GetRequest("/api/Accounts");

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)                
               ;
        }

        [Fact]
        public async Task ShouldGetCustomers()
        {
            var request = new GetRequest("/api/Customer");

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }

        [Fact]
        public async Task ShouldSaveCustomer()
        {
            var request = new PostRequest("/api/Customer", new { 
                                  name = "test",
                                  surname = "customer",
                                  initialCredit = 0
                });

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200)
               ;
        }
        [Fact]
        public async Task ShouldSaveAccount()
        {
            var request = new PostRequest("/api/Accounts", new
            {
                customerid = "testid",
                name = "default",
                initialCredit = 0
            });

            await client
                .MakeRequestAsync(request)
                .ExpectStatus(200);
        }
    }
}
