namespace CoreService.Data.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Coordinate { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}