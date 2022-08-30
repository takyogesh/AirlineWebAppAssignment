namespace AirlineWebApp.ApiModel
{
    public class AirLineApiModel
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FromCity { get; set; }
        public string? ToCity { get; set; }
        public int Fare { get; set; }
    }
}
