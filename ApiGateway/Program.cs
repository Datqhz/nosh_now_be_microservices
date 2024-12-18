using ApiGateway.Extensions;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


ServiceExtensions serviceExtensions = new ServiceExtensions(builder.Configuration);
serviceExtensions.ConfigureAuthentication(builder.Services);
serviceExtensions.AddAuthorizationSettings(builder.Services);
serviceExtensions.AddCustomCors(builder.Services);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Routes/ocelot.swaggerentpoint.json", optional: false, reloadOnChange: true);
// builder.Configuration.AddOcelotWithSwaggerSupport(options =>
// {
//     options.Folder = "Routes";
//     options.PrimaryOcelotConfigFileName = "ocelot.swaggerentpoint.json"; 
// });

builder.Services.AddOcelot(builder.Configuration);
serviceExtensions.ConfigureSwagger(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
  // app.UseSwaggerForOcelotUI();
}

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();
app.Run();