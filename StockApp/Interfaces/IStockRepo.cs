using StockApp.DTOs;
using StockApp.Entities;
namespace StockApp.Interfaces
{
    public interface IStockRepo
    {
        public IEnumerable<Stock> GetStocks();
        public Stock GetStockById(int id);
        public IEnumerable<Stock> GetStockByStockname(string[] names);
    }
}
