namespace Shared.Responses;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
    public string MessageCode { get; set; }
}