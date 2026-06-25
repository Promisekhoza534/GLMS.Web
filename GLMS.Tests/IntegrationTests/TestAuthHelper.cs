using System.Net.Http.Json;
using System.Text.Json;

namespace GLMS.Tests.IntegrationTests
{
    public static class TestAuthHelper
    {

        public static async Task AddJwtToken(HttpClient client)
        {

            var login = new
            {
                Username = "admin",
                Password = "admin123"
            };


            var response =
                await client.PostAsJsonAsync(
                    "/api/auth/login",
                    login);


            response.EnsureSuccessStatusCode();


            var json =
                await response.Content.ReadAsStringAsync();


            using var doc =
                JsonDocument.Parse(json);


            var token =
                doc.RootElement
                .GetProperty("token")
                .GetString();


            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

        }

    }
}