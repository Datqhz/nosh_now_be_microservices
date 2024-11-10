using Shared.Responses;

namespace AuthServer.Models.Responses;

public class LoginResponse : BaseResponse
{
    public LoginResponseData Data { get; set; }
}

public class LoginResponseData
{
    public string AccessToken { get; set; }
    public string Scope { get; set; }
    public int Expired  { get; set; }
}