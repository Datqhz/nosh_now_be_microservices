using AuthServer.Data.Models;
using Shared.Enums;
using Shared.Responses;

namespace AuthServer.Models.Responses;

public class RegisterResponse : BaseResponse
{
    public RegisterPostProcessorPayload Data { get; set; }
}

public class RegisterPostProcessorPayload
{
    public Account Account { get; set; }
    public string Avatar { get; set; }
    public string DisplayName { get; set; }
    public string? Coordinate { get; set; }
    public SystemRole Role { get; set; }
}