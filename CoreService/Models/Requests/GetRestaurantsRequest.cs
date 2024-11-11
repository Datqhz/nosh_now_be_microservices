namespace CoreService.Models.Requests;

public class GetRestaurantsRequest
{
    public string Keyword { get; set; } = String.Empty;
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
    public string Coordinate { get; set; }
}