using Dapper;
using StockApp.Context;
using StockApp.Entities;
using StockApp.Interfaces;



namespace StockApp.Repository
{
    public class StockPriceRepo : IStockPriceRepo
    {
        private readonly StockDbContext _context;
        public StockPriceRepo(StockDbContext context)
        {
            _context = context;
        }
        public IEnumerable<StockPrice> GetStockPrices()
        {
            var query = @"select * from [stockPrice] sp join [stock] s on sp.stockid=s.stockid";
            using(var connection= _context.createConnection())
            {
                var stockprices= connection.Query<StockPrice,Stock,StockPrice>(query,
                    (stockPrice,stock)=>
                {
                    stockPrice.Stock=stock;
                    return stockPrice;
                },splitOn:"stockid");
                return stockprices;
            }
        }
    }
}
