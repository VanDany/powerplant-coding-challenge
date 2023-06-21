using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace powerplant.API.Models
{
    public class Fuel
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public decimal Gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public decimal Kerosine { get; set; }
        [JsonPropertyName("wind(%)")]
        public decimal Wind { get; set; }
    }
}
