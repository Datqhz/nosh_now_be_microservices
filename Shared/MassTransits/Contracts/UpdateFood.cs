namespace Shared.MassTransits.Contracts;

public record UpdateFood
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
}