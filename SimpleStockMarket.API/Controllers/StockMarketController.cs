using Microsoft.AspNetCore.Mvc;
using SimpleStockMarket.API.Stocks;

namespace SimpleStockMarket.API.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockMarketController : ControllerBase
    {

        

        private readonly StockService _stockService;

        public StockMarketController(StockService stockService)
        {
            
            _stockService = stockService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStockData(String symbol, int? numberOfDays = null)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return BadRequest("Stock symbol is required.");
            }

            StockPriceResponse? stockData = await _stockService.GetAlphaVantageDataAsync(symbol, numberOfDays);

            if (stockData == null || !stockData.Prices.Any())
            {
                return NotFound($"No stock data found for {symbol} in the last {numberOfDays ?? 0} days.");
            }

            return Ok(stockData);
        }
    }
}
