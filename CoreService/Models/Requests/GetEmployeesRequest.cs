using Shared.Enums;

namespace CoreService.Models.Requests;

public class GetEmployeesRequest
{
    public RestaurantRole Role { get; set; }
    public string Keyword { get; set; } = String.Empty;
    public int PageNumber { get; set; }
    public int MaxPerPage { get; set; }
}
