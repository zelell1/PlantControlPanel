using Microsoft.AspNetCore.Mvc;
using PlantControlPanel.Application.Contracts.RollService;
using PlantControlPanel.Application.Contracts.RollService.Operations;

namespace Presentation.Controllers;

[ApiController]
[Route("api/rolls")]
public class RollsController : ControllerBase
{
    private readonly IRollService _rollService;

    public RollsController(IRollService rollService)
    {
        _rollService = rollService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddRoll.Request request, CancellationToken ct)
    {
        var response = await _rollService.AddRoll(request, ct);

        return response switch
        {
            AddRoll.Response.Success s => Ok(s.Roll), 
            AddRoll.Response.BadRequest b => BadRequest(b.Error),
            _ => StatusCode(500, "Unknown error")
        };
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
    {
        var request = new DeleteRoll.Request(id);
        
        var response = await _rollService.DeleteRoll(request, ct);

        return response switch
        {
            DeleteRoll.Response.Success s => Ok(s.Roll),
            DeleteRoll.Response.BadRequest b => BadRequest(b.Error), 
            _ => StatusCode(500, "Unknown error")
        };
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCatalog([FromQuery] GetRollsCatalog.Request request, 
        CancellationToken ct)
    {
        var response = await _rollService.RollsCatalog(request, ct);

        return response switch
        {
            GetRollsCatalog.Response.Success s => Ok(s.Catalog),
            GetRollsCatalog.Response.BadRequest b => BadRequest(b.Error),
            _ => StatusCode(500, "Unknown error")
        };
    }
    
    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics([FromQuery] GetStatistics.Request request, 
        CancellationToken ct)
    {
        var response = await _rollService.Statistics(request, ct);

        return response switch
        {
            GetStatistics.Response.Success s => Ok(s.Statistics),
            GetStatistics.Response.BadRequest b => BadRequest(b.Error),
            _ => StatusCode(500, "Unknown error")
        };
    }
}