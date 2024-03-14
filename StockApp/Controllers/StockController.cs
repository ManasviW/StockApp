using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Interfaces;
using StockApp.CustomException;
namespace StockApp.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;
        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _stockRepo.GetStocks();
            return Ok(stocks);
        }
        [HttpGet("{id}",Name ="StockbyId")]
        public IActionResult GetStock(int id)
        {
            var stock= _stockRepo.GetStockById(id);
            if (stock == null)
                return NotFound();
            return Ok(stock);
        }
        [HttpGet("byname")]
        public IActionResult getStockbyName([FromQuery]string name)
        {
            String[] names= name.Split(',');
            var stocks= _stockRepo.GetStockByStockname(names);
           // Console.WriteLine(stocks);
            if (stocks == null)
               return NotFound(name);
            return Ok(stocks);
        }

    }
}
