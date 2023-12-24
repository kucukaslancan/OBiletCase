using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class ClientSession
    {
        [JsonPropertyName("session-id")]
        public string Id { get; set; }

        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }
    }
}
