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
        public Stock GetStockById(int id) {
            var query = "select * from Stock where stockid= @Id";
            using(var connection = _context.createConnection())
            {
                var stock = connection.QuerySingle<Stock>(query, new {id});
                return stock;
            }
        }

       public IEnumerable<Stock> GetStockByStockname(string name)
        {
            var query = "select * from stock where stockName like @Name ";
            using(var connection = _context.createConnection())
            {
                var Name = name +"%";
                var stocks= connection.Query<Stock>(query, new { Name });
                return stocks.ToList();
            }
        }
    }
}
