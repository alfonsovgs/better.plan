using Better.Application.Queries;
using Better.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Better.Infrastructure.Data;

internal static class Extensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext<ChallengeContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SqlConnection"));
            })
            .AddScoped<IUserQueryService, UserQueryService>();
}
