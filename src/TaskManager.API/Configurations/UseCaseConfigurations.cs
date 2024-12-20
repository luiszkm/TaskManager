using TaskManager.Application.UseCases.User.Create;
using TaskManager.Domain.Authorization;
using TaskManager.Domain.Repositories;
using TaskManager.Infra.EFCore.Authorization;
using TaskManager.Infra.EFCore.Persistence.Repository;

namespace TaskManager.API.Configurations;

public static class UseCaseConfigurations
{

    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateUser).Assembly));
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ITasksRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorization, Authorization>();
        return services;
    }


}