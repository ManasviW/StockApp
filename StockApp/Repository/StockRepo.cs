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

       public IEnumerable<Stock> GetStockByStockname(string[] names)
        {
            var query = "select * from stock where ";
            for(int i = 0;i<names.Length;i++)
            {
                if(i>0)
                    query += " or ";
                query += $"stockName= '{names[i]}'";
            }
            using(var connection = _context.createConnection())
            {
                Console.WriteLine(query);
                var stocks= connection.Query<Stock>(query);
                return stocks.ToList();
            }
        }
    }
}
