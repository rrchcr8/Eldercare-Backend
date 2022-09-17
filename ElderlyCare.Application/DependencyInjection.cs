using ElderlyCare.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElderlyCare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
