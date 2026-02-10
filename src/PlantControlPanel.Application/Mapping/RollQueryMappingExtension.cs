using PlantControlPanel.Application.Abstractions.Persistence.Queries;
using PlantControlPanel.Application.Contracts.RollService.Operations;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Application.Mapping;

public static class RollQueryMappingExtension
{
    public static RollQuery ToQuery(this GetRollsCatalog.Request request)
        => RollQuery.Build(x => x
            .WithMinId(request.MinId)
            .WithMaxId(request.MaxId)
            .WithMinLength(request.MinLength)
            .WithMaxLength(request.MaxLength)
            .WithMinWeight(request.MinWeight)
            .WithMaxWeight(request.MaxWeight)
            .WithMinAddTime(request.MinAddTime)
            .WithMaxAddTime(request.MaxAddTime)
            .WithMinRemoveTime(request.MinRemoveTime)
            .WithMaxRemoveTime(request.MaxRemoveTime));
}