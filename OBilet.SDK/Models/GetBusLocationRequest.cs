using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class GetBusLocationRequest
    {
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        [JsonPropertyName("device-session")]
        public ClientSession DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}