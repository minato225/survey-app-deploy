using Microsoft.Extensions.DependencyInjection;
using SurveyApp.Application.Forms;
using SurveyApp.Application.Users;
using System.Text.Json.Serialization;

namespace SurveyApp.Application;

public static class ApplicationInjector
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IFormService, FormService>()
            ;
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        return services;
    }
}