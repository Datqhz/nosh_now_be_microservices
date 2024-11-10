using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace CoreService.Data.Models;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public string Avatar { get; set; }
    public string Coordinate { get; set; }
    public string AccountId { get; set; }
    public virtual IEnumerable<Calendar> Calendars { get; set; }
    public virtual IEnumerable<Employee> Employees { get; set; }
    public virtual IEnumerable<Food> Foods { get; set; }
    public virtual IEnumerable<Ingredient> Ingredients { get; set; }
}