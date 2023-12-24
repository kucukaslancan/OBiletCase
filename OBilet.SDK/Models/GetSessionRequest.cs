using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class GetSessionRequest
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("connection")]
        public ClientConnection Connection { get; set; }

        [JsonPropertyName("browser")]
        public ClientBrowser Browser { get; set; }
    }
}
