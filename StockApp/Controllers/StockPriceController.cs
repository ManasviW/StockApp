using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.CustomException;
using StockApp.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("id")]
        public IActionResult GetStockPricesbyId([FromQuery]int id)
        {
            try
            {
                var stocks = _stockPriceRepo.GetStockPricesbyId(id);
                return Ok(stocks);  
            }catch(NotFoundException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }
        [HttpGet("average")]
        public IActionResult GetStockbyAvg()
        {
            var stocks= _stockPriceRepo.GetAverageStockbyDay();
            return Ok(stocks);
        }
        [HttpGet("pricenotdefine")]
        public IActionResult GetStocksNotHavingPrice()
        {
            var stocks= _stockPriceRepo.GetSTockNotHavingPrice();
            if(stocks.Count()>0)
                return Ok(stocks);
            throw new NotFoundException();
        }

        [HttpGet("bydate")]
        public IActionResult GetByDate([FromQuery] int id, [FromQuery] DateTime date)
        {
            try
            {
                //string[] strings = date.Split('-');
                //Console.WriteLine(strings[0], strings[1], strings[2]);
                // DateTime date1 = new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));
                var stockprices = _stockPriceRepo.GetStockPriceByDate(id, date);
                return Ok(stockprices);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }
        [HttpGet("getprices")]
        public IActionResult GetPricesPerDay()
        {
            var stocks= _stockPriceRepo.GetStockPricesPerDay();
            if(stocks.Count()>0 )
                return Ok(stocks);
            else
                throw new NotFoundException();
        }
        [HttpPost("byrange")]
        public IActionResult GetByRange([FromBody] int id, DateTime from, DateTime to)
        {
            try
            {
                //string[] strings = date.Split('-');
                //Console.WriteLine(strings[0], strings[1], strings[2]);
                // DateTime date1 = new DateTime(int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]));
                var stockprices = _stockPriceRepo.GetStockPriceByRange(id, from, to);
                return Ok(stockprices);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }
       /* [HttpGet("notprice")]
        public IActionResult GetStockWithoutPrice()
        {
            try
            {
                var stocks = _stockPriceRepo.GetStocksNotPrice();
                return Ok(stocks);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }*/
        [HttpGet("highest3")]
        public IActionResult GetHighestThree()
        {
            var stockprices = _stockPriceRepo.GetThreeHighest();
            return Ok(stockprices);
        }
    }
}
