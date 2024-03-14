using StockApp.Entities;

namespace StockApp.Interfaces
{
    public interface IStockPriceRepo
    {
        public IEnumerable<StockPrice> GetStockPrices();
        public IEnumerable<StockPrice> GetStockPriceByDate(int id, DateTime date);
    }
}
