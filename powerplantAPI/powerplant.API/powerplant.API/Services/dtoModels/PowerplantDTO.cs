using powerplant.API.Models;

namespace powerplant.API.Services.dtoModels
{
    public class PowerplantDTO
    {
        public string Name { get; set; }
        public PowerPlantType Type { get; set; }
        public decimal Efficiency { get; set; }
        public decimal Pmin { get; set; }
        public decimal Pmax { get; set; }
        public decimal RealCost { get; set; }
        public decimal RealPower { get; set; }
    }
}
