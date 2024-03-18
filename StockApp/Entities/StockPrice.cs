namespace StockApp.Entities
{
    public class StockPrice
    {
        public int Stockpriceid {  get; set; }
        public decimal Price {  get; set; }
        public DateTime? CreatedAT {  get; set; }
        public DateTime? UpdatedAT { get; set;}
        public int? Stockid { get; set; }
        public Stock Stock { get; set; }
    }
}
