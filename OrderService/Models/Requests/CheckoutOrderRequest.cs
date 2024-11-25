using OrderService.Data.Models;
using OrderService.Enums;

namespace OrderService.Models.Requests;

public class CheckoutOrderRequest
{
    public long OrderId { get; set; }
    public DeliveryInfo DeliveryInfo { get; set; }
    public string PaymentMethod { get; set; }
    public decimal ShippingFee { get; set; }
}

public class CheckoutOrderDetailData
{
    public int FoodId { get; set; }
    public int Amount { get; set; }
    public ModifyOption Option { get; set; }
}