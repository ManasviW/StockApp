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
            try
            {
                var stock = _stockRepo.GetStockById(id);
                if (stock == null)
                    throw new NotFoundException();
                return Ok(stock);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }
        [HttpGet("byname")]
        public IActionResult getStockbyName([FromQuery] string name)
        {
            try
            {
                String[] names = name.Split(',');
                var stocks = _stockRepo.GetStockByStockname(names);
                return Ok(stocks);
            }
            catch (NotFoundException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(200, "Element not Found");
            }
        }

    }
}
