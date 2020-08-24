using System;
using System.Text;
using System.Text.Json.Serialization;

namespace buildxact_supplies.Services.Megacorp
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Supply    {
        [JsonPropertyName("id")]
        public int Id { get; set; } 

        [JsonPropertyName("description")]
        public string Description { get; set; } 

        [JsonPropertyName("uom")]
        public string Uom { get; set; } 

        [JsonPropertyName("priceInCents")]
        public int PriceInCents { get; set; } 

        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; } 

        [JsonPropertyName("materialType")]
        public string MaterialType { get; set; } 
    }
}
