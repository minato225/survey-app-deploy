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
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(100),
                            errorCodesToAdd: null);
                        
                        npgsqlOptions.CommandTimeout(300); // 5 minutes
                    }));
        ;

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IFormRepository, FormRepository>()
            ;

        return services;
    }
}