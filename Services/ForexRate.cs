namespace UsersApp.Services
{
    public class ForexRate
    {
        public string Currency { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
    public class ForexApiResponse
    {
        public List<ForexRate> Data { get; set; }
    }
}
