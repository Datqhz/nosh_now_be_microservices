namespace OrderService.Services;

public enum SimulationResult
{
    NoShipperFree,
    ShipperSkip,
    TimeoutReceiveOrder,
    Complete
}