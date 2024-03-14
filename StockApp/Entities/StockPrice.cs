namespace StockApp.Entities
{
    public class StockPrice
    {
        private int Stockpriceid {  get; set; }
        private decimal? Price {  get; set; }
        private DateTime? CreatedAT {  get; set; }
        private DateTime? UpdatedAT { get; set;}
        private int? Stockid { get; set; }
    }
}
