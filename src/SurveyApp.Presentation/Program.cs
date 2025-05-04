using Microsoft.EntityFrameworkCore;
using SurveyApp.Presentation;
using SurveyApp.Application;
using SurveyApp.ExternalServices;
using SurveyApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExternalServices()
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices()
    .AddPresentation();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseCors("AllowAll");

app.UseCookiePolicy();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.MigrateAsync();
}

app.Run();