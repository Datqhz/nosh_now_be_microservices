using Microsoft.AspNetCore.Identity;

namespace AuthServer.Data.Models;

public class Account : IdentityUser
{
    public int ClientId { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; } =  true;
}