using Shared.Enums;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetRestaurantProfileResponse : BaseResponse
{
    public GetRestaurantProfileData Data { get; set; }
}

public class GetRestaurantProfileData
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Avatar { get; set; }
    public string Coordinate { get; set; }
}