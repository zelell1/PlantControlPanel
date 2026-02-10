using PlantControlPanel.Application.Abstractions.Persistence.Queries;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Application.Abstractions.Persistence.Repositories;

public interface IRollRepository
{
    Task<Roll> Add(Roll roll, CancellationToken ct);
    
    Task Update(Roll roll, CancellationToken ct = default);
    
    Task<Roll> Delete(Roll roll, CancellationToken ct = default);
    
    IQueryable<Roll> Query(RollQuery query);
}