using Microsoft.EntityFrameworkCore;
using PlantControlPanel.Application.Abstractions.Persistence.Queries;
using PlantControlPanel.Application.Abstractions.Persistence.Repositories;
using PlantControlPanel.Domain;
using PlantControlPanel.Infrastructure.Mapping;
using PlantControlPanel.Infrastructure.Persistence.Context;

namespace PlantControlPanel.Infrastructure.Persistence.Repositories;

public class RollRepository : IRollRepository
{   
    private readonly RollDbContext _context;
    
    public RollRepository(RollDbContext context)
    {
        _context = context;
    }

    public async Task<Roll> Add(Roll roll, CancellationToken ct = default)
    {
        await _context.Rolls.AddAsync(roll, ct);
        await _context.SaveChangesAsync(ct);
        
        return roll;
    }

    public async Task Update(Roll roll, CancellationToken ct = default)
    {
        _context.Rolls.Update(roll);
        await _context.SaveChangesAsync(ct);
    }
    
    public async Task<Roll> Delete(Roll roll, CancellationToken ct)
    {
        await Update(roll, ct);
        return roll; 
    }

    public IQueryable<Roll> Query(RollQuery query)
    {
        return _context.Rolls
            .AsNoTracking()
            .WhereIf(query.MinId.HasValue, x => x.Id >= query.MinId)
            .WhereIf(query.MaxId.HasValue, x => x.Id <= query.MaxId)
            .WhereIf(query.MinWeight.HasValue, x => x.Weight >= query.MinWeight)
            .WhereIf(query.MaxWeight.HasValue, x => x.Weight <= query.MaxWeight)
            .WhereIf(query.MinLength.HasValue, x => x.Length >= query.MinLength)
            .WhereIf(query.MaxLength.HasValue, x => x.Length <= query.MaxLength)
            .WhereIf(query.MinAddTime.HasValue, x => x.AddTime >= query.MinAddTime)
            .WhereIf(query.MaxAddTime.HasValue, x => x.AddTime <= query.MaxAddTime) 
            .WhereIf(query.MinRemoveTime.HasValue, x => x.RemoveTime >= query.MinRemoveTime)
            .WhereIf(query.MaxRemoveTime.HasValue, x => x.RemoveTime <= query.MaxRemoveTime);
    }
}