using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public class UpdateUser
{
    public string AccountId { get; set; }
    public bool IsActive { get; set; }
    public SystemRole SystemRole { get; set; }
}