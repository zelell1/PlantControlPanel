using PlantControlPanel.Application.Contracts.RollService.Models;

namespace PlantControlPanel.Application.Contracts.RollService.Operations;

public static class AddRoll
{
    public readonly record struct Request(double Length, double Weight);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(RollDto Roll) : Response { }

        public sealed record BadRequest(string Error) : Response { }
    }
}