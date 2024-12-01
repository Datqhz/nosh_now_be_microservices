using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreService.Data.Models;

public class Location
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Coordinate { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}