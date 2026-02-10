using PlantControlPanel.Application.Abstractions.Persistence;
using PlantControlPanel.Application.Abstractions.Persistence.Queries;
using PlantControlPanel.Application.Contracts.RollService;
using PlantControlPanel.Application.Contracts.RollService.Operations;
using PlantControlPanel.Application.Mapping;
using PlantControlPanel.Domain;
using Microsoft.EntityFrameworkCore;
using PlantControlPanel.Application.Contracts.RollService.Models;

namespace PlantControlPanel.Application.Services;

public class RollService : IRollService
{
    private readonly IPersistenceContext _context;

    public RollService(IPersistenceContext context)
    {
        _context = context;
    }

    public async Task<AddRoll.Response> AddRoll(AddRoll.Request request,  CancellationToken ct)
    {
        try 
        {
            if (request.Length <= 0 || request.Weight <= 0)
            {
                return new AddRoll.Response.BadRequest("Length or weight must be greater than 0");
            }
            
            var roll = new Roll(0, request.Length, request.Weight, DateTime.UtcNow);
            
            var savedRoll = await _context.RollRepository.Add(roll, ct);
        
            return new AddRoll.Response.Success(savedRoll.MapToDto());
        }
        catch (Exception ex)
        {
            return new AddRoll.Response.BadRequest(ex.Message);
        }
    }

    public async Task<DeleteRoll.Response> DeleteRoll(DeleteRoll.Request request, CancellationToken ct)
    {
        try
        {
            var roll = await _context.RollRepository
                .Query(RollQuery.Build(x => x.WithMinId(request.Id).WithMaxId(request.Id)))
                .FirstOrDefaultAsync(ct);

            if (roll == null)
            {
                return new DeleteRoll.Response.BadRequest($"Roll with id {request.Id} not found");
            }
            
            if (roll.RemoveTime != null)
            {
                return new DeleteRoll.Response.BadRequest($"Roll with id {request.Id} is already deleted");
            }
            
            roll.RemoveTime = DateTime.UtcNow;
            
            await _context.RollRepository.Delete(roll, ct);
            
            return new DeleteRoll.Response.Success(roll.MapToDto());
            
        } catch (Exception ex)
        {
            return new DeleteRoll.Response.BadRequest(ex.Message);
        }
    }

    public async Task<GetRollsCatalog.Response> RollsCatalog(GetRollsCatalog.Request request, CancellationToken ct)
    {
        try
        {
            IQueryable<Roll> rolls = _context.RollRepository.Query(request.ToQuery());

            var rollsList = await rolls.ToListAsync(ct);
            
            return new GetRollsCatalog.Response.Success(rollsList.MapToDto());
        }
        catch (Exception ex)
        {
            return new GetRollsCatalog.Response.BadRequest(ex.Message);
        }
    }

    public async Task<GetStatistics.Response> Statistics(GetStatistics.Request request, CancellationToken ct)
    {
        try
        { 
            var startDate = request.StartDate ?? DateTime.MinValue;
            var endDate = request.EndDate ?? DateTime.MaxValue;
            
            if (startDate > endDate)
            {
                return new GetStatistics.Response.BadRequest("Start date cannot be later than End date.");
            }
            
            var rolls = await _context.RollRepository.Query(RollQuery.Build(x => x))
                .Where(x => x.AddTime <= endDate && 
                           (x.RemoveTime == null || x.RemoveTime >= startDate))
                .ToListAsync(ct);
            
            if (rolls.Count == 0)
            {
                return new GetStatistics.Response.Success(
                    new StatisticsDto(0, 0, 0, 0,
                        0, 0, 0, 0, 
                        0, null, null, null, 
                        null, null, null));
            }
            
            var addedCount = rolls.Count(x => x.AddTime >= startDate && x.AddTime <= endDate);
            var removedCount = rolls.Count(x => x.RemoveTime.HasValue && x.RemoveTime.Value >= startDate 
                                                                      && x.RemoveTime.Value <= endDate);

            var avgLength = rolls.Average(x => x.Length);
            var avgWeight = rolls.Average(x => x.Weight);
            var minLength = rolls.Min(x => x.Length);
            var maxLength = rolls.Max(x => x.Length);
            var minWeight = rolls.Min(x => x.Weight);
            var maxWeight = rolls.Max(x => x.Weight);
            var totalWeight = rolls.Sum(x => x.Weight);


            var durations = rolls
                .Where(x => x.RemoveTime.HasValue)
                .Select(x => x.RemoveTime.Value - x.AddTime)
                .ToList();

            TimeSpan? minDuration = durations.Any() ? durations.Min() : null;
            TimeSpan? maxDuration = durations.Any() ? durations.Max() : null;

            if (endDate == DateTime.MaxValue)
                endDate = DateTime.MaxValue.AddDays(-1);
            
            var dailyStats = new List<(DateTime Date, int Count, double Weight)>();
            for (var day = startDate; day <= endDate; day = day.AddDays(1))
            {
                var onWarehouseThisDay = rolls.Where(x => 
                    x.AddTime.Date <= day && 
                    (x.RemoveTime == null || x.RemoveTime.Value.Date > day)).ToList();
                
                dailyStats.Add((day, onWarehouseThisDay.Count, onWarehouseThisDay.Sum(r => r.Weight)));
            }

            DateTime? dayMinCount = dailyStats.Any() ? dailyStats.MinBy(x => x.Count).Date : null;
            DateTime? dayMaxCount = dailyStats.Any() ? dailyStats.MaxBy(x => x.Count).Date : null;
            DateTime? dayMinWeight = dailyStats.Any() ? dailyStats.MinBy(x => x.Weight).Date : null;
            DateTime? dayMaxWeight = dailyStats.Any() ? dailyStats.MaxBy(x => x.Weight).Date : null;
            
            var statsDto = new StatisticsDto(
                addedCount,
                removedCount,
                avgLength,
                avgWeight,
                minLength,
                maxLength,
                minWeight,
                maxWeight,
                totalWeight,
                minDuration,
                maxDuration,
                dayMinCount,
                dayMaxCount,
                dayMinWeight,
                dayMaxWeight
            );

            return new GetStatistics.Response.Success(statsDto);
        }
        catch (Exception ex)
        {
            return new GetStatistics.Response.BadRequest(ex.Message);
        }
    }
}