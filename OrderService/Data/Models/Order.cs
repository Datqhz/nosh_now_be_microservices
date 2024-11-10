using OrderService.Enums;

namespace OrderService.Data.Models;

public class Order
{
    public long Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal ShippingFee { get; set; }
    public DeliveryInfo? DeliveryInfo { get; set; }
    public OrderStatus Status { get; set; }
    public string CustomerId { get; set; }
    public string RestaurantId { get; set; }
    public string? ShipperId { get; set; }
    public Guid? PaymentMethodId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual PaymentMethod? PaymentMethod { get; set; }
    public virtual Shipper? Shipper { get; set; }
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
}

public class DeliveryInfo
{
    public string Phone { get; set; }
    public string Coordinate { get; set; }
}