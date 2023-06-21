using Newtonsoft.Json;
using powerplant.API.Models;
using powerplant.API.Services.dtoModels;
using powerplant.API.Services.Interfaces;
using System.Linq.Expressions;

namespace powerplant.API.Services
{
    public class PowerplantService : IPowerplantService
    {
        public List<PowerplantResponse> GetProductionPlan(PowerplantRequest request)
        {
            decimal load = request.Load;

            List<PowerplantResponse> resList = new List<PowerplantResponse>();
            
            List<PowerplantDTO> powerplantsSorted = GetMeritOrder(request);


            foreach (var powerplant in powerplantsSorted)
            {
                PowerplantResponse res = new PowerplantResponse();
                res.Name = powerplant.Name;
                if (powerplant.RealPower <= load && load > 0)
                {
                    res.P = powerplant.RealPower;
                    load -= res.P;
                }
                else
                {   
                    //if Pmin > load, don't use this powerplant and check the following one. Load is unchanged. 
                    if (powerplant.Pmin > load)
                    {
                        res.P = 0;
                    }
                    //Last powerplant to complete the load
                    else
                    {
                        res.P = load;
                        load = 0;
                    }
                }

                resList.Add(res);
            }
            return resList;
        }
        private List<PowerplantDTO> GetMeritOrder(PowerplantRequest request)
        {
            List<PowerplantDTO> list = new List<PowerplantDTO>();

            foreach (Powerplant element in request.Powerplants)
            {
                decimal realCost = RealCost(request.Fuels, element);
                decimal realPower = Math.Round(RealPower(request.Fuels, element), 1);
                PowerplantDTO powerplantWithCost = new PowerplantDTO
                    {
                    Name = element.Name,
                    Type = element.Type,
                    Efficiency = element.Efficiency,
                    Pmin  = element.Pmin,
                    Pmax = element.Pmax,
                    RealCost = realCost,
                    RealPower = realPower
                };
                list.Add(powerplantWithCost);
            }

            return list
                .OrderBy(i => i.RealCost)
                .ThenByDescending(i => i.RealPower)
                .ThenByDescending(i => i.Efficiency)
                .ToList();
        }

        private decimal RealPower(Fuel fuel, Powerplant element)
        {
            if (element.Type == PowerPlantType.WindTurbine)
            {
                return element.Pmax / 100 * fuel.Wind;
            }
            else return element.Pmax;
        }

        private decimal RealCost(Fuel fuel, Powerplant element)
        {
            decimal energyCost = 0;
            switch (element.Type)
            {
                case PowerPlantType.GasFired:
                    energyCost = fuel.Gas;
                    break;
                case PowerPlantType.TurboJet:
                    energyCost = fuel.Kerosine;
                    break;
                case PowerPlantType.WindTurbine:
                    energyCost = 0.0m;
                    break;
            }
            return energyCost / element.Efficiency;
        }
    }
}
