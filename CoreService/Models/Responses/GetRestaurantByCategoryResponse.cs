using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetRestaurantByCategoryResponse : BaseResponse
{
    public List<GetRestaurantByCategoryData> Data { get; set; }
}

public class GetRestaurantByCategoryData
{
    public string RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public double Distance { get; set; }
    public string Avatar { get; set; }
}