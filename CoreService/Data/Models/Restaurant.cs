﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreService.Data.Models.Interfaces;
using Shared.Enums;

namespace CoreService.Data.Models;

public class Restaurant : IUser
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
    public virtual IEnumerable<Calendar> Calendars { get; set; }
    public virtual IEnumerable<Employee> Employees { get; set; }
}