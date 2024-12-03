namespace CommunicationService.Constants;

public class OrderNotificationCustomerContent
{
    public const string OrderPreparing =
        "Your order {0} order from {1} is preparing\nYou will be notified preparing process.";
    public const string OrderRejected =
        "Your order {0} order from {1} was rejected\nPlease try again later.";
    public const string OrderReadyToPickup =
        "Your order {0} order from {1} already to delivery";
    public const string OrderDelivering =
        "Your order {0} order from {1} is delivering\nYou can see process in maps.";
    public const string OrderArrived =
        "Your order {0} order from {1} arrived\nPlease take your order.";
    public const string DeliverSuccess =
        "Order {0} deliverd";
}

public class OrderNotificationServiceStaffContent
{
    public const string OrderCheckedOut =
        "Your restaurant have a new order {0}\nPlease accept or reject this order";
    public const string OrderReadyToPickup =
        "Order {0} prepare process done\n Please bring it to shipper";
    public const string OrderArrived =
        "Shipper deliver order {0} is arrived delivery address";
    public const string OrderFailed =
        "Your order {0} order don't have any customer take it.";
    public const string DeliverSuccess =
        "Your order {0} was delivered successfully.";
}

public class OrderNotificationChefContent
{
    public const string OrderPreparing =
        "Your have new order {0} need to prepare it\nPLease handel it soon";
}

public class OrderNotificationCustomerTitle
{
    public const string OrderPreparing = "Order is preparing";
    public const string OrderRejected = "Order is rejected";
    public const string OrderReadyToPickup = "Order is ready to pickup";
    public const string OrderDelivering = "Order is delivering";
    public const string OrderArrived = "Order is arrived";
    public const string DeliverSuccess = "Deliver success";
}
public class OrderNotificationServiceStaffTitle
{
    public const string OrderCheckedOut = "Have a new order";
    public const string OrderReadyToPickup = "Order {0} is prepared";
    public const string OrderArrived = "Shipper is arrived delivery address";
    public const string OrderFailed = "Order is failed";
    public const string DeliverSuccess = "Deliver success";
}
public class OrderNotificationChefTitle
{
    public const string OrderPreparing = "Have a order need prepare";
}