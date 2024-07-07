using Microsoft.AspNetCore.Mvc;
using Tariffs.Calculation;
using Tariffs.Host.Api;

namespace Tariffs.Host.Controllers;

[ApiController]
[Route("api/tariffs/costs")]
public class TariffCostsController : ControllerBase
{
    private readonly TariffCostsService _service;

    public TariffCostsController(TariffCostsService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Calculates the costs for the given consumption using all available tariffs.
    /// </summary>
    /// <param name="consumptionPerYear">Amount of kWh used per year.</param>
    /// <returns>List of tariffs and theirs costs.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestResult<IEnumerable<TariffCost>>))]
    [ProducesResponseType<string>(StatusCodes.Status400BadRequest, Type = typeof(RequestResult<IEnumerable<TariffCost>>))]
    public ActionResult<RequestResult<IEnumerable<TariffCost>>> Get(int consumptionPerYear)
    {
        if (consumptionPerYear <= 0)
        {
            var error = new Error("Consumption should be greater than 0");
            return BadRequest(new RequestResult<IEnumerable<TariffCost>>(new Error[] {error}));
        }
        
        var costs = _service.CalculateCosts(consumptionPerYear);
        
        return Ok(new RequestResult<IEnumerable<TariffCost>>(costs.OrderBy(x => x.AnnualCost)));
    }
}

