using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace buildxact_supplies.Services.Megacorp
{
    public class MegacorpDataModel    {
        [JsonPropertyName("partners")]
        public List<Partner> Partners { get; set; } 
    }
}