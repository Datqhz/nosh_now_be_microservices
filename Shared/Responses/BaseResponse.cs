namespace Shared.Responses;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
    public string MessageCode { get; set; }
}

public enum ResponseStatusCode
{
    Ok = 200,
    Created = 201,
    InternalServerError = 500,
    Forbidden = 403,
    BadRequest = 400,
    NotFound = 404,
}