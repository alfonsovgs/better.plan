using Better.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Better.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services
            .ConfigureDbContext();

        return services;
    }

}