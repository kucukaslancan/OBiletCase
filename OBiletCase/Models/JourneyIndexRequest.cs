namespace OBiletCase.Models
{
    public class JourneyIndexRequest
    {
        public int Origin { get; set; }
        public int Destination { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
