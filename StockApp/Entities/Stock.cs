namespace StockApp.Entities
{
    public class Stock
    {
        public int stockid { get; set; }
        public string stockName { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public List<StockPrice> prices { get; set; }= new List<StockPrice>();

    }
}
