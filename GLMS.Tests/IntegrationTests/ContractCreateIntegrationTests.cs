using System.Net;
using System.Net.Http.Json;
using GLMS.API.Models;
using Xunit;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GLMS.Tests.IntegrationTests
{
    public class ContractCreateIntegrationTests
    {

        private readonly HttpClient client;


        public ContractCreateIntegrationTests()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7250")
            };

            var token = GenerateJwtToken();


            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }



        [Fact]
        public async Task CreateContract_Returns201()
        {

            // STEP 1:
            // Create a client first because Contract requires ClientId


            var newClient = new
            {
                companyName = "Test Company",
                email = "test@test.com",
                phoneNumber = "123456789",
                address = "Test Address",
                region = "Gauteng"
            };


            var clientResponse =
                await client.PostAsJsonAsync(
                    "/api/Clients",
                    newClient);



            Assert.Equal(
                HttpStatusCode.Created,
                clientResponse.StatusCode);



            var createdClient =
                await clientResponse.Content
                .ReadFromJsonAsync<Client>();


            Assert.NotNull(createdClient);



            // STEP 2:
            // Create contract using the real ClientId


            var contract = new
            {
                contractNumber = "TEST001",

                clientId = createdClient!.ClientId,

                startDate = DateTime.Now,

                endDate = DateTime.Now.AddMonths(12),

                status = "Pending",

                serviceLevel = "Premium"
            };



            var response =
                await client.PostAsJsonAsync(
                    "/api/Contracts",
                    contract);



            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

        }

        private string GenerateJwtToken()
        {
            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        "GLMS_SUPER_SECRET_KEY_2026_CHANGE_THIS"));


            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);



            var claims = new[]
            {
        new Claim(
            JwtRegisteredClaimNames.Sub,
            "test-user"),

        new Claim(
            ClaimTypes.Name,
            "Integration Tester")
    };



            var token =
                new JwtSecurityToken(

                    issuer: "GLMS.API",

                    audience: "GLMS.Web",

                    claims: claims,

                    expires:
                    DateTime.UtcNow.AddMinutes(30),

                    signingCredentials:
                    credentials);



            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

    }
}