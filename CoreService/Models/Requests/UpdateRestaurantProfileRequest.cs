namespace CoreService.Models.Requests;

public class UpdateRestaurantProfileRequest
{
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public string Coordinate { get; set; }
}