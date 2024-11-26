namespace CommunicationService.Constants;

public class OrderNotificationContent
{
    public const string OrderPreparing =
        "Your order {{orderId}} order from {{restaurant}} is preparing\nYou will be notified preparing process.";
    public const string OrderRejected =
        "Your order {{orderId}} order from {{restaurant}} was rejected\nPlease try again later.";
    public const string OrderReadyToPickup =
        "Your order {{orderId}} order from {{restaurant}} already to delivery";
    public const string OrderDelivering =
        "Your order {{orderId}} order from {{restaurant}} is delivering\nYou can see process in maps.";
    public const string OrderArrived =
        "Your order {{orderId}} order from {{restaurant}} arrived\nPlease take your order.";
    public const string OrderFailed =
        "Your order {{orderId}} order don't have any customer take.";
    public const string HaveANewOrder =
        "Your restaurant has a new order.";
    public const string DeliverSuccess =
        "Your order {{orderId}} was delivered successfully.";
}

public class OrderNotificationTitle
{
    public const string OrderPreparing = "Order is preparing";
    public const string OrderRejected = "Order is rejected";
    public const string OrderReadyToPickup = "Order is ready to pickup";
    public const string OrderDelivering = "Order is delivering";
    public const string OrderArrived = "Order is arrived";
    public const string OrderFailed = "Order is failed";
    public const string HaveANewOrder = "Have a new order";
    public const string DeliverSuccess = "Deliver success";
}