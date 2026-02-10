using PlantControlPanel.Application.Contracts.RollService.Models;

namespace PlantControlPanel.Application.Contracts.RollService.Operations;

public static class DeleteRoll
{
    public readonly record struct Request(int Id);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(RollDto Roll) : Response { }

        public sealed record BadRequest(string Error) : Response { }
    }
}