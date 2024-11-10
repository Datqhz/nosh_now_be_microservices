namespace Shared.MassTransits.Contracts;

public record SendVerificationEmail
{
    public string Email { get; init; }
    public string VerificationToken { get; init; }
    public string DisplayName { get; init; }
}