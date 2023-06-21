using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;

namespace powerplant.API.Models
{
    public class PowerplantRequest
    {
        public decimal Load { get; set; } 
        [JsonProperty("fuels")]
        public Fuel Fuels { get; set; }
        public IEnumerable<Powerplant> Powerplants { get; set; }
    }
}
