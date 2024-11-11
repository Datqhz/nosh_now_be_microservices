using Shared.Models.Dtos;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetRestaurantsResponse : BaseResponse
{
    public List<GetRestaurantsData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetRestaurantsData
{
    public string RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public double Distance { get; set; }
    public string Avatar { get; set; }
}