using CoreService.Extensions;
using Shared.HttpContextAccessor;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureDbContext(builder.Configuration)
    .ConfigureDependencyInjection()
    .AddCustomAuthentication()
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
app.UseCors("AllowAnyOrigin");
//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();