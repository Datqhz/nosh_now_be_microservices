using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public class CreateSnapshotUser
{
    public string Id { get; init; }
    public string DisplayName { get; init; }
    public string Avatar { get; init; }
    public string Phone {get; init;}
    public SystemRole Role { get; init; }
    public string? Coordinate { get; set; }
}