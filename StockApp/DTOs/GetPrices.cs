namespace StockApp.DTOs
{
    public class GetPrices
    { 
      //  public string stockName { get; set; }
        public int stockid { get; set; }
        public DateTime created { get; set; }
        public decimal price { get; set; }  
        public string stockname { get; set; }
    }
}
