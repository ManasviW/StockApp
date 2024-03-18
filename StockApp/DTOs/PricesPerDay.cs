namespace StockApp.DTOs
{
    public class PricesPerDay
    {
        public int stockid { get; set; }
        public DateTime date { get; set; }
        public decimal first_price {  get; set; }
        public decimal last_price { get; set;}
    }
}
