using Microsoft.Extensions.Logging;
using SimpleStockMarket.API.Stocks;

namespace SimpleStockMarket.API.Controllers
{
    public sealed class StockService(StocksClient stocksClient,
    ILogger<StockService> logger)
    {
        public async Task<StockPriceResponse?> GetAlphaVantageDataAsync(string symbol, int? numberOfDays = null)
        {
            try
            {
                // Fetch data from the external API
                StockPriceResponse? apiPrice = await stocksClient.GetDataForSymbol(symbol);

                if (apiPrice == null)
                {
                    logger.LogWarning("No data returned from external API for ticker: {Ticker}", symbol);
                    return null;
                }

                // If numberOfDays is specified, filter the stock prices
                if (numberOfDays.HasValue)
                {
                    DateTime cutoffDate = DateTime.UtcNow.AddDays(-numberOfDays.Value);
                    List<StockEntry> filteredPrices = apiPrice.Prices
                        .Where(entry => entry.Date >= cutoffDate)
                        .ToList();

                    return new StockPriceResponse(symbol, filteredPrices);
                }

                // If no filtering is needed, return the entire dataset
                return apiPrice;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while fetching stock price for ticker: {Ticker}", symbol);
                throw;
            }
        }
    }
}