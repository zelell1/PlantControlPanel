using Microsoft.Extensions.DependencyInjection;
using PlantControlPanel.Application.Contracts.RollService;
using PlantControlPanel.Application.Services;

namespace PlantControlPanel.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IRollService, RollService>();

        return collection;
    }
}