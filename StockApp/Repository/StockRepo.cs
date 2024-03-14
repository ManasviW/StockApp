using StockApp.Context;
using StockApp.Interfaces;
using StockApp.Entities;
using Dapper;
namespace StockApp.Repository
{
    public class StockRepo: IStockRepo
    {
        private readonly StockDbContext? _context;
        public StockRepo(StockDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Stock> GetStocks()
        {
            var query = "select * from stock";
                using (var connection = _context.createConnection())
                {
                    var stocks = connection.Query<Stock>(query);
                    return stocks.ToList();
                }
            
        }
    }
}
