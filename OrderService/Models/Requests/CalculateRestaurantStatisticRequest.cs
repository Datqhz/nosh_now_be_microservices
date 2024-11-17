namespace OrderService.Models.Requests;

public class CalculateRestaurantStatisticRequest
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}