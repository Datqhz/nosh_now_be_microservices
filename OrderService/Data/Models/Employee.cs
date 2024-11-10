using Shared.Enums;

namespace OrderService.Data.Models;

public class Employee
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public RestaurantRole Role { get; set; }
}