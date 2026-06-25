using System.Net;
using Xunit;


namespace GLMS.Tests.IntegrationTests
{

    public class ContractsApiTests
    {

        private readonly HttpClient client;


        public ContractsApiTests()
        {

            client = new HttpClient();

            client.BaseAddress =
                new Uri("http://localhost:7250");

        }



        [Fact]
        public async Task GetContracts_Returns200OK()
        {

            await TestAuthHelper.AddJwtToken(client);



            var response =
                await client.GetAsync(
                    "/api/Contracts");



            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);


        }



    }

}