using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public record UpdateSnapshotUser
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string? Coordinate { get; init; }
    public string? Phone { get; init; }
    public string Avatar { get; init; }
    public SystemRole Role { get; init; }
}