using AuthServer.Models.Responses;

namespace AuthServer.Models.Requests;

public class ChangePasswordRequest 
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
