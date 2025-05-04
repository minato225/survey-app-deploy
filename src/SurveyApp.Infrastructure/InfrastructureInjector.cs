using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Infrastructure.Repositories.Forms;
using SurveyApp.Infrastructure.Repositories.Users;

namespace SurveyApp.Infrastructure;

public static class InfrastructureInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 7,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    }));
        ;

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IFormRepository, FormRepository>()
            ;

        return services;
    }
}