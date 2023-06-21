using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace powerplant.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PowerPlantType
    {
        [Description("gasfired")]
        GasFired,
        [Description("turbojet")]
        TurboJet,
        [Description("windturbine")]
        WindTurbine
    }
}
