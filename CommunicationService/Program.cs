using CommunicationService.Extensions;
using Shared.HttpContextAccessor;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.ConfigureDbContext(builder.Configuration)
    .ConfigureDependencyInjection()
    .AddValidators()
    .ConfigureMediator()
    .AddCustomCors()
    .AddCustomHttpContextAccessor()
    .ConfigureSwagger()
    //.AddCustomMassTransitRegistration()
    .AddSignalRServer();
builder.Services.AddControllers();
var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("AllowAllOrigins");
//app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseAuthorization();
app.AddSignalREndpoints();
app.Run();