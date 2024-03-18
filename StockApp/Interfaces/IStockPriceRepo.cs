using StockApp.DTOs;
using StockApp.Entities;

namespace StockApp.Interfaces
{
    public interface IStockPriceRepo
    {
        public IEnumerable<Stock> GetStockPrices();
        public Stock GetStockPricesbyId(int id);
        public IEnumerable<GetPrices> GetAverageStockbyDay();
        public IEnumerable<StockPrice> GetStockPriceByDate(int id, DateTime date);
        public IEnumerable<StockPrice> GetStockPriceByRange(int id, DateTime from, DateTime to);
        //public IEnumerable<Stock> GetStocksNotPrice();
        public IEnumerable<StockPrice> GetThreeHighest();
        public IEnumerable<Stock> GetSTockNotHavingPrice();
        public IEnumerable<PricesPerDay> GetStockPricesPerDay();
    }
}
