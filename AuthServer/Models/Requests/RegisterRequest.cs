using Shared.Constants;

namespace AuthServer.Models.Requests;

public class RegisterRequest
{
    public string Displayname { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; } = Shared.Constants.Constants.ImageDefault.AvatarDefault;
}