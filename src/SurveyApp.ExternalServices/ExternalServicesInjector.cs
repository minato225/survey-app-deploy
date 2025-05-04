using Microsoft.Extensions.DependencyInjection;
using SurveyApp.ExternalServices.Abstractions;
using SurveyApp.ExternalServices.Auth;
using SurveyApp.ExternalServices.Options;

namespace SurveyApp.ExternalServices;

public static class ExternalServicesInjector
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services
            .AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions));

        services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenService, TokenService>()
            ;

        return services;
    }
}