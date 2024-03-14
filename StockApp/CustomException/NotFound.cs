namespace StockApp.CustomException
{
    public class NotFoundException: Exception
    {
        public override string Message
        {
            get
            {
                return "Element Not Found";
            }
        }
    }
}
