using System.Text.Json;

namespace GLMS.Web.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertUsdToZar(decimal amountUsd)
        {
            var response = await _httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/USD");

            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<ExchangeRateResponse>(json);

            var rate = data?.rates["ZAR"] ?? 0;

            return amountUsd * rate;
        }
    }

    public class ExchangeRateResponse
    {
        public Dictionary<string, decimal> rates { get; set; } = new();
    }
}