using powerplant.API.Models;

namespace powerplant.API.Services.Interfaces
{
    public interface IPowerplantService
    {
        public List<PowerplantResponse> GetProductionPlan(PowerplantRequest request);
    }
}
