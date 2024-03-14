using StockApp.Entities;
namespace StockApp.Interfaces
{
    public interface IStockRepo
    {
        public IEnumerable<Stock> GetStocks();
    }
}
