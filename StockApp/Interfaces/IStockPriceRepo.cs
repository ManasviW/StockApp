using StockApp.Entities;

namespace StockApp.Interfaces
{
    public interface IStockPriceRepo
    {
        public IEnumerable<StockPrice> GetStockPrices();
    }
}
