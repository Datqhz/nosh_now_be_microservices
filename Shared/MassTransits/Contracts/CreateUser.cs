using Shared.Enums;

namespace Shared.MassTransits.Contracts;

public record CreateUser
{
    public string Id { get; init; }
    public string DisplayName { get; init; }
    public string Email { get; init; }
    public string Avatar { get; init; }
    public string PhoneNumber {get; init;}
    public SystemRole Role { get; init; }
}