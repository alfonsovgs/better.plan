using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Better.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
