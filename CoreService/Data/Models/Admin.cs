using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreService.Data.Models.Interfaces;

namespace CoreService.Data.Models;

public class Admin : IUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string Avatar { get; set; }
}