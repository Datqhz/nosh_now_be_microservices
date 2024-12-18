﻿namespace OrderService.Data.Models;

public class Shipper
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public bool IsFree { get; set; } = true;
    public virtual IEnumerable<Order> Orders { get; set; }
}