namespace OrderService.Models.Requests;

public class CalculateAdminStatisticRequest
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}