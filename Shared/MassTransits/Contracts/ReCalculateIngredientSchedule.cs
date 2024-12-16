namespace Shared.MassTransits.Contracts;

public class ReCalculateIngredientSchedule
{
    public long OrderId { get; set; }
    public TimeSpan Duration { get; set; }
}