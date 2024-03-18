using Dapper;
using StockApp.Context;
using StockApp.CustomException;
using StockApp.DTOs;
using StockApp.Entities;
using StockApp.Interfaces;
using System.Linq;

namespace StockApp.Repository
{
    public class StockPriceRepo : IStockPriceRepo
    {
        private readonly StockDbContext _context;
        private List<StockPrice> _stockPrices;
        public StockPriceRepo(StockDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Stock> GetStockPrices()
        {
            var query = @"SELECT *
                  FROM [stockPrice] sp 
                  JOIN [stock] s ON s.stockid = sp.stockid";
           
            using (var connection = _context.createConnection())
            {
                Dictionary<int, List<StockPrice>> dict= new Dictionary<int, List<StockPrice>>();    
                var stocks = connection.Query<StockPrice, Stock,Stock>(query,
                    (stockprice, stock) =>
                    {
                        if (stock.stockid == stockprice.Stockid)
                        {
                            if (dict.ContainsKey(stock.stockid))
                                dict[stock.stockid].Add(stockprice);
                            else
                            {
                                stock.stockPrice.Add(stockprice);
                                dict.Add(stock.stockid, stock.stockPrice);
                            }
                        }
                        return stock;
                    },
                    splitOn: "stockid" // Not needed in this case
                );

               
                // Transform result into GetPrices objects
               List<Stock> stockslist= stocks.ToList();
                if(stockslist.Count > 0)
                return stockslist;
                else
                    throw new NotFoundException();
            }
        }
        public Stock GetStockPricesbyId(int id)
        {
            var query = @"SELECT *
                  FROM [stockPrice] sp 
                  JOIN [stock] s ON s.stockid = sp.stockid where s.stockid=@id";
            using (var connection = _context.createConnection())
            {
                Dictionary<int, List<StockPrice>> dict = new Dictionary<int, List<StockPrice>>();
                var stocks = connection.Query<StockPrice, Stock, Stock>(query,
                    (stockprice, stock) =>
                    {
                        if (stock.stockid == stockprice.Stockid)
                        {
                            if (dict.ContainsKey(stock.stockid))
                                dict[stock.stockid].Add(stockprice);
                            else
                            {
                                stock.stockPrice.Add(stockprice);
                                dict.Add(stock.stockid, stock.stockPrice);
                            }
                        }
                        return stock;
                    },new {id},
                    splitOn: "stockid" 
                );
                return stocks.First();
                
            }
        }
        public IEnumerable<GetPrices> GetAverageStockbyDay()
        {
            var query = @"select s.stockName stockname ,sp.stockid,convert(date, sp.CreatedAt) created, avg(Price) price from stockprice sp 
                           join stock s on sp.stockid=s.stockid group by s.stockname,sp.stockid,convert(date,sp.CreatedAt)";
            using(var connection = _context.createConnection()) 
            {
                var stocks = connection.Query<GetPrices>(query);
                return stocks;
            }
        }
        public IEnumerable<Stock> GetSTockNotHavingPrice()
        {
            var query = "select s.* from stock s left join stockPrice sp on s.stockid = sp.stockid where price is null";
            using(var connection= _context.createConnection())
            {
                var stocks= connection.Query<Stock>(query);
                return stocks.ToList();
            }
        }













        IEnumerable<StockPrice> IStockPriceRepo.GetStockPriceByDate(int id, DateTime date)
        {
            var query = @"select * from [stockprice] sp join [stock] s on sp.stockid=s.stockid where sp.stockid=@id and convert(date,sp.createdAT)=@date";
            using (var conn = _context.createConnection())
            {
                var stockprices = conn.Query<StockPrice, Stock, StockPrice>(query,
                    (stockprice, stock) =>
                    {
                        stockprice.Stock = stock;
                        return stockprice;
                    }, new { id, date }, splitOn: "stockid");
                List<StockPrice> list = stockprices.ToList();
                if (list.Count == 0)
                    throw new NotFoundException();
                else
                    return list;
            }
        }

        public IEnumerable<StockPrice> GetStockPriceByRange(int id, DateTime from, DateTime to)
        {

            var query = @"select * from [stockprice] sp join [stock] s on sp.stockid=s.stockid where sp.stockid=@id and convert(date,sp.createdAT) between @from and @to";
            using (var conn = _context.createConnection())
            {
                var stockprices = conn.Query<StockPrice, Stock, StockPrice>(query,
                    (stockprice, stock) =>
                    {
                        stockprice.Stock = stock;
                        return stockprice;
                    }, new { id, from, to }, splitOn: "stockid");
                List<StockPrice> list = stockprices.ToList();
                if (list.Count == 0)
                    throw new NotFoundException();
                else
                    return list;
            }
        }

       /* public IEnumerable<Stock> GetStocksNotPrice()
        {
            var query = @"select * from [stock] s left join [stockprice] sp on s.stockid = sp.stockid where price is null";
            using (var conn = _context.createConnection())
            {
                var stocks = conn.Query<Stock, StockPrice, Stock>(query,
                    (stock, stockprice) =>
                    {
                        stockprice.Stock = stock;
                        stock.stockPrice.Add(stockprice);
                        return stockprice.Stock;
                    }, splitOn: "stockid");
                List<Stock> list = stocks.ToList();
                if (list.Count == 0)
                    throw new NotFoundException();
                else return list;
            }
        }*/

        public IEnumerable<StockPrice> GetThreeHighest()
        {
            var query = @"select * from [stockPrice] sp join [stock] s on s.stockid= sp.stockid where price in 
  (select distinct top 3 price from stockprice order by price desc) order by price";
            using (var conn = _context.createConnection())
            {
                var stockprices = conn.Query<StockPrice, Stock, StockPrice>(query,
                    (stockprice, stock) =>
                    {
                        stockprice.Stock = stock;
                        return stockprice;
                    }, splitOn: "stockid");
                List<StockPrice> list = stockprices.ToList<StockPrice>();
                if (list.Count == 0)
                    throw new NotFoundException();
                else
                {
                    return stockprices;
                }

            }
        }

        public IEnumerable<PricesPerDay> GetStockPricesPerDay()
        {
            var query = @"select 
    date,stockid,
    min(case when  fdate = 1 then price end) as first_price, min(case when  ldate = 1 then price end) as last_price from ( select stockid,
        convert(date, createdAt) as date, price,
        ROW_NUMBER() over (PARTITION BY CONVERT(date, createdAt),stockid ORDER BY createdAt) as fdate,
        ROW_NUMBER() over (PARTITION BY CONVERT(date, createdAt),stockid ORDER BY createdAt desc) as ldate from stockprice) as prices
        group by stockid,date";
            using(var conn = _context.createConnection())
            {
                var stocks = conn.Query<PricesPerDay>(query);
                if(stocks.Count()>0)
                return stocks;
                return null;
            }
        }
    }
}
