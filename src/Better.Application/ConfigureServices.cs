using Better.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Better.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .AddScoped<IBalanceService, BalanceService>()
            .AddScoped<ICurrencyIndicatorLookup, CurrencyIndicatorLookup>();

        return services;
    }
}
