using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class GetJourneyRequest
    {

        [JsonPropertyName("device-session")]
        public ClientSession DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("data")]
        public JourneyRequestData Data { get; set; }
    }

    public class JourneyRequestData
    {
        [JsonPropertyName("origin-id")]
        public int OriginId { get; set; }

        [JsonPropertyName("destination-id")]
        public int DestinationId { get; set; }

        [JsonPropertyName("departure-date")]
        public DateTime DepartureDate { get; set; }
    }
}
