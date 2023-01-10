using Microsoft.Extensions.DependencyInjection;

namespace Better.Infrastructure.Data;

internal static class Extensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        => services
            .AddDbContext<ChallengeContext>();
}
