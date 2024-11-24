using AuthServer.Extensions;
using Shared.HttpContextAccessor;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureDbContext(builder.Configuration)
    .ConfigureDependencyInjection()
    .ConfigureIdentityServer()
    .AddAuthorizationSettings()
    .AddValidators()
    .ConfigureMediator()
    .AddCustomCors()
    .AddCustomHttpContextAccessor()
    .ConfigureSwagger()
    .AddCustomMassTransitRegistration();
builder.Services.AddControllers();
var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowAllOrigins");
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();