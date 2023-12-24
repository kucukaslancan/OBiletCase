using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class GetBusLocationResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<BusLocationData> Data { get; set; }

    }

    public class BusLocationData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("long-name")]
        public string LongName { get; set; }
    }
}