using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Infrastructure.Auth;
using GodsOfStruggle.Infrastructure.Persistence;
using GodsOfStruggle.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GodsOfStruggle.Infrastructure.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => npgsql.EnableRetryOnFailure(3)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
