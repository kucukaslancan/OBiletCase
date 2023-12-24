using System.Text.Json.Serialization;

namespace OBilet.SDK.Models
{
    public class GetJourneysResponse
    {

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<JourneysData> Data { get; set; }
    }

    public class JourneysData
    {
        public int Id { get; set; }

        [JsonPropertyName("partner-id")]
        public int PartnerId { get; set; }

        [JsonPropertyName("partner-name")]
        public string PartnerName { get; set; }

        [JsonPropertyName("origin-location")]
        public string OriginLocation { get; set; }

        [JsonPropertyName("destination-location")]
        public string DestinationLocation { get; set; }

        [JsonPropertyName("journey")]
        public JourneyData Journey { get; set; }

    }

    public class JourneyData
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("departure")]
        public DateTime Departure { get; set; }

        [JsonPropertyName("arrival")]
        public DateTime Arrival { get; set; }

        [JsonPropertyName("original-price")]
        public decimal OriginalPrice { get; set; }

        [JsonPropertyName("internet-price")]
        public decimal InternetPrice { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
