using ElderlyCare.Application.Common.Interfaces.Authentication;
using ElderlyCare.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ElderlyCare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}