using PlantControlPanel.Application.Contracts.RollService.Models;

namespace PlantControlPanel.Application.Contracts.RollService.Operations;

public static class GetStatistics
{
    public sealed record Request
    {
        public DateTime? StartDate { get; init; } = null;
        public DateTime? EndDate { get; init; } = null;
        
        public Request() { }
        
        public Request(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(StatisticsDto Statistics) : Response { }

        public sealed record BadRequest(string Error) : Response { }
    }
}