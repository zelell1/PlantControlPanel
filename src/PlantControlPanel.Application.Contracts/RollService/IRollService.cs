using PlantControlPanel.Application.Contracts.RollService.Operations;

namespace PlantControlPanel.Application.Contracts.RollService;

public interface IRollService
{
    Task<AddRoll.Response> AddRoll(AddRoll.Request request, CancellationToken ct);
    
    Task<DeleteRoll.Response> DeleteRoll(DeleteRoll.Request request, CancellationToken ct);
    
    Task<GetRollsCatalog.Response> RollsCatalog(GetRollsCatalog.Request request, CancellationToken ct);
    
    Task<GetStatistics.Response> Statistics(GetStatistics.Request request, CancellationToken ct);
}