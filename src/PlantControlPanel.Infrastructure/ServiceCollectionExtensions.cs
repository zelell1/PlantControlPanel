using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantControlPanel.Application.Abstractions.Persistence;
using PlantControlPanel.Application.Abstractions.Persistence.Repositories;
using PlantControlPanel.Infrastructure;
using PlantControlPanel.Infrastructure.Persistence.Context;
using PlantControlPanel.Infrastructure.Persistence.Repositories;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        Action<DbContextOptionsBuilder> dbOptionsAction)
    {
        services.AddDbContext<RollDbContext>(dbOptionsAction);
        
        services.AddScoped<IPersistenceContext, PersistenceContext>();
        services.AddScoped<IRollRepository, RollRepository>();

        return services;
    }
}