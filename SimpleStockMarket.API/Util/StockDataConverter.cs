using SimpleStockMarket.API.Controllers;
using SimpleStockMarket.API.Stocks;

namespace SimpleStockMarket.API.Util
{
    public static class StockDataConverter
    {
        public static StockPriceResponse ConvertToStockPriceData(AlphaVantageData alphaVantageData, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (alphaVantageData == null || alphaVantageData.MetaData == null || alphaVantageData.TimeSeriesDaily == null)
                throw new ArgumentNullException("Invalid AlphaVantageData object.");

            string symbol = alphaVantageData.MetaData.Symbol;
            var stockEntries = new List<StockEntry>();

            foreach (var entry in alphaVantageData.TimeSeriesDaily)
            {
                DateTime date = DateTime.Parse(entry.Key);
                var priceData = entry.Value;

                // Apply optional filtering based on date range
                if (startDate.HasValue && date < startDate.Value) continue;
                if (endDate.HasValue && date > endDate.Value) continue;

                stockEntries.Add(new StockEntry(
                    date,
                    priceData.Open,
                    priceData.High,
                    priceData.Low,
                    priceData.Close,
                    priceData.Volume
                ));
            }

            return new StockPriceResponse(symbol, stockEntries);
        }
    }
}
