using Xunit;

namespace GLMS.Tests
{
    public class CurrencyCalculationTests
    {
        [Fact]
        public void ConvertUsdToZar_ReturnsCorrectAmount_WhenRateIsProvided()
        {
            
            decimal amountUsd = 100;
            decimal exchangeRate = 18.50m;

            
            decimal amountZar = amountUsd * exchangeRate;

            
            Assert.Equal(1850.00m, amountZar);
        }
    }
}