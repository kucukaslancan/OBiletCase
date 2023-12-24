using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class ClientConnection
    {
        [JsonPropertyName("ip-address")]
        public string IpAddress { get; set; }

        [JsonPropertyName("port")]
        public string Port { get; set; }
    }
}
