using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Interfaces;

namespace StockApp.Controllers
{
    [Route("api/stockprices")]
    [ApiController]
    public class StockPriceController : ControllerBase
    {
        private readonly IStockPriceRepo _stockPriceRepo;
        public StockPriceController(IStockPriceRepo stockPriceRepo)
        {
            _stockPriceRepo = stockPriceRepo;
        }
        [HttpGet]
        public IActionResult GetStockPrices()
        {
            var stockprices= _stockPriceRepo.GetStockPrices();
            return Ok(stockprices);
        }
        [HttpGet("bydate")]
        public IActionResult GetByDate([FromQuery]int id,[FromQuery]string date)
        {
            string[] strings = date.Split('-');
            Console.WriteLine(strings[0], strings[1], strings[2]);
            DateTime date1 = new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));
            var stockprices= _stockPriceRepo.GetStockPriceByDate(id, date1);
            if(stockprices == null)
                return NotFound();
            return Ok(stockprices);
        }
    }
}
