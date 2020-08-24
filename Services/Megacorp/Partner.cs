using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace buildxact_supplies.Services.Megacorp
{
    public class Partner    {
        [JsonPropertyName("name")]
        public string Name { get; set; } 

        [JsonPropertyName("partnerType")]
        public string PartnerType { get; set; } 

        [JsonPropertyName("partnerAddress")]
        public string PartnerAddress { get; set; } 

        [JsonPropertyName("supplies")]
        public List<Supply> Supplies { get; set; } 
    }
}