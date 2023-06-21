using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using powerplant.API.Models;
using powerplant.API.Services.Interfaces;
using powerplant.API.Logging;
using System;

namespace powerplant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerPlantController : ControllerBase
    {
        private readonly IPowerplantService _PowerplantService;
        private readonly ILogging _logger;
        public PowerPlantController(IPowerplantService powerplantService, ILogging logger)
        {
            _PowerplantService = powerplantService;
            _logger = logger;
        }
        [HttpPost("productionplan")]
        public IActionResult ProductionPlan([FromBody] PowerplantRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("You have to send a request");
                }
                if (request.Load == 0)
                {
                    _logger.Log("Load can't be 0 or negative", "error");
                    return BadRequest("Load can't be 0 or negative");
                }
                else return Ok(_PowerplantService.GetProductionPlan(request));

            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
