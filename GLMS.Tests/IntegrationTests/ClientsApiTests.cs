using System.Net;
using Xunit;

namespace GLMS.Tests.IntegrationTests
{
    public class ClientsApiTests
    {

        private readonly HttpClient _client;


        public ClientsApiTests()
        {
            _client = new HttpClient();

            _client.BaseAddress =
                new Uri("http://localhost:7250");
        }



        [Fact]
        public async Task GetClients_Returns200OK()
        {

            await TestAuthHelper.AddJwtToken(_client);



            var response =
                await _client.GetAsync(
                    "/api/Clients");



            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

        }

    }
}