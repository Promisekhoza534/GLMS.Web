using System.Net;
using Xunit;


namespace GLMS.Tests.IntegrationTests
{

    public class ServiceRequestsApiTests
    {


        private readonly HttpClient client;



        public ServiceRequestsApiTests()
        {

            client = new HttpClient();

            client.BaseAddress =
                new Uri("http://localhost:7250");

        }




        [Fact]
        public async Task GetServiceRequests_Returns200OK()
        {


            await TestAuthHelper.AddJwtToken(client);



            var response =
                await client.GetAsync(
                    "/api/ServiceRequests");



            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);


        }


    }

}