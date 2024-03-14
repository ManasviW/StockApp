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
    }
}
