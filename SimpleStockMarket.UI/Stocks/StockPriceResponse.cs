namespace SimpleStockMarket.UI.Stocks
{
    public record StockPriceResponse(string Symbol, List<StockEntry> Prices);

    public record StockEntry(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, long Volume);
}
