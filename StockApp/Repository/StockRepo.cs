using StockApp.Context;
using StockApp.Interfaces;
using StockApp.Entities;
using StockApp.CustomException;
using Dapper;
namespace StockApp.Repository
{
    public class StockRepo: IStockRepo
    {
        private readonly StockDbContext _context;
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
                    List<Stock> list= stocks.ToList<Stock>();
                if (list.Count == 0)
                    throw new NotFoundException();
                else
                    return list;
                }
            
        }
        public Stock GetStockById(int Id) {
            try
            {
                var query = "select * from Stock where stockid= @Id";
                using (var connection = _context.createConnection())
                {

                    var stock = connection.QuerySingle<Stock>(query, new { Id });
                  
                    return stock;
                }
            }catch (InvalidOperationException ex)
            {
                return null;
            }
        }

       public IEnumerable<Stock> GetStockByStockname(string[] names)
        {
            var query = "select * from stock where ";
            //var parameters = new DynamicParameters();
            for (int i = 0;i<names.Length;i++)
            {
                if(i>0)
                    query += " or ";
                query += $"stockName= '{names[i]}'";
            }
            using(var connection = _context.createConnection())
            {
                Console.WriteLine(query);
                var stocks= connection.Query<Stock>(query);
                List<Stock> list = stocks.ToList<Stock>();
                if (list.Count == 0)
                    throw new NotFoundException();
                else
                    return list;
            }
        }
    }
}
