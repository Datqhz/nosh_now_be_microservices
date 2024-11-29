namespace OrderService.Models.Requests;

public class GetRestaurantsByCategoryRequest
{
    public string CategoryId { get; set; }
    public string Coordinate { get; set; }
    public int PageNumber { get; set; }
    public int MaxPerPage { get; set; }
}