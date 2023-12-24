using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class ClientBrowser
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
