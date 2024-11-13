namespace CoreService.Models.Requests;

public class GetRestaurantByCategoryRequest
{
    public string CategoryId { get; set; }
    public string Coordinate { get; set; }
}