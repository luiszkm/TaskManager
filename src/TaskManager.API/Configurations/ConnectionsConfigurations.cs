using Microsoft.EntityFrameworkCore;
using TaskManager.Infra.EFCore;

namespace TaskManager.API.Configurations;

public static class ConnectionsConfigurations
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbConnectionDev(config);
        //services.AddInMemoryConnections();

        return services;
    }

    private static IServiceCollection AddDbConnectionDev(
       this IServiceCollection services,
       IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Postgress");
        services.AddDbContext<TaskManegDbContext>(
            options => options.UseNpgsql(connectionString));

        return services;
    }
    public static IServiceCollection AddInMemoryConnections(
        this IServiceCollection services)
    {
        services.AddDbContext<TaskManegDbContext>(
            options => options
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("Db-In-Memory-dev"));

        return services;
    }




}