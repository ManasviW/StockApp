using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Interfaces;

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
            var stocks= _stockRepo.GetStocks();
            return Ok(stocks);
        }
    }
}
