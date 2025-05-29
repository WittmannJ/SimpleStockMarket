
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SimpleStockMarket.API.Stocks;
using SimpleStockMarket.API.Util;

namespace SimpleStockMarket.API.Controllers
{
    public sealed class StocksClient(
        HttpClient httpClient,
        IConfiguration configuration,
        IMemoryCache memoryCache,
        ILogger<StocksClient> logger)
    {
        public async Task<StockPriceResponse?> GetDataForSymbol(string symbol)
        {
            logger.LogInformation("Getting stock price information for {Symbol}", symbol);

            StockPriceResponse? stockPriceResponse = await memoryCache.GetOrCreateAsync($"stocks-{symbol}", async entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(24*60)); // only try to re-new stock data after 24 hours

                return await GetStockPrice(symbol);
            });

            if (stockPriceResponse is null)
            {
                logger.LogWarning("Failed to get stock price information for {Symbol}", symbol);
            }
            else
            {
                logger.LogInformation(
                    "Completed getting stock price information for {Symbol}", symbol);
            }
            return stockPriceResponse;
        }

        private async Task<StockPriceResponse?> GetStockPrice(string symbol)
        {
            string symbolDataString = await httpClient.GetStringAsync($"?function=TIME_SERIES_DAILY&symbol={symbol}&outputsize=full&apikey={configuration["Stocks:ApiKey"]}");

            AlphaVantageData? symbolData = JsonConvert.DeserializeObject<AlphaVantageData>(symbolDataString);

            // filter to only include the last three years
            //DateTime threeYearsAgo = DateTime.Today.AddYears(-3);


            StockPriceResponse formattedData = StockDataConverter.ConvertToStockPriceData(symbolData);

            return formattedData;
        }
    }
}