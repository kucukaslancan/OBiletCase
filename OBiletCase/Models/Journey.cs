namespace OBiletCase.Models
{
    public class Journey
    {
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }
}
