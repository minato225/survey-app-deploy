using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace SurveyApp.Presentation;

public static class PresentationInjector
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddCors(CorsOptions);

        services
            .AddAuthentication(AuthenticationOptions)
            .AddJwtBearer(JwtOptions)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, CookieOptions);

        services.AddAuthorization(AuthOptions);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static void AuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    private static void AuthOptions(AuthorizationOptions options)
    {
        var authorizationPolicyBuilder = new AuthorizationPolicyBuilder(
            JwtBearerDefaults.AuthenticationScheme,
            CookieAuthenticationDefaults.AuthenticationScheme);
        var multiSchemePolicy = authorizationPolicyBuilder
            .RequireAuthenticatedUser()
            .Build();

        options.DefaultPolicy = multiSchemePolicy;
    }

    private static void CookieOptions(CookieAuthenticationOptions options)
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        };
    }

    private static void JwtOptions(JwtBearerOptions options)
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey("Rk8vZs#Q7dLpX3!mB9g@Yt4^WcJhN2sE"u8.ToArray())
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Cookies["AccessToken"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    }

    private static void CorsOptions(CorsOptions options)
    {
        options.AddPolicy("AllowAll", b => b
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );
    }
}