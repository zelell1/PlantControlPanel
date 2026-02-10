using PlantControlPanel.Application.Contracts.RollService.Models;
using SourceKit.Generators.Builder.Annotations;

namespace PlantControlPanel.Application.Contracts.RollService.Operations;

public static class GetRollsCatalog
{
    [GenerateBuilder]
    public sealed partial record Request(
        int? MinId = null,
        int? MaxId = null,
        double? MinWeight = null,
        double? MaxWeight = null,
        double? MinLength = null,
        double? MaxLength = null,
        DateTime? MinAddTime = null,
        DateTime? MaxAddTime = null,
        DateTime? MinRemoveTime = null,
        DateTime? MaxRemoveTime = null
    );

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(RollsCatalogDto Catalog) : Response { }

        public sealed record BadRequest(string Error) : Response { }
    }
}