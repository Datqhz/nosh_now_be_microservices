using System.Text.Json;
using Shared.Responses;
using Shared.Validations;

namespace CoreService.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)ResponseStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = new 
            {
                status = (int)ResponseStatusCode.BadRequest,
                statusText = "",
                errorMessage = "Invalid data value",
                errorMessageCode = JsonSerializer.Serialize(ex.Errors),
            };
            await context.Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(response));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)ResponseStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = new 
            {
                status = (int)ResponseStatusCode.BadRequest,
                statusText = "",
                errorMessage = "Invalid data value",
                errorMessageCode = JsonSerializer.Serialize(ex),
            };
            await context.Response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(response));
        }
        
    }
}