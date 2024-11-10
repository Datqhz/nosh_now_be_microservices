using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreService.Data.Models;

public class Shipper
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Phone { get; set; }
    public string Avatar { get; set; }
    public string Coordinate { get; set; }
    public bool Status { get; set; } // is free or busy
}