using ElderlyCare.Application.Common.Interfaces.Authentication;
using ElderlyCare.Application.Common.Interfaces.Persistence;
using ElderlyCare.Application.Common.Interfaces.Services;
using ElderlyCare.Infrastructure.Authentication;
using ElderlyCare.Infrastructure.Persistance;
using ElderlyCare.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElderlyCare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}