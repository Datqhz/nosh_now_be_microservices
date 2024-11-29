using Shared.Models.Dtos;
using Shared.Responses;

namespace OrderService.Models.Responses;

public class GetRestaurantsByCategoryResponse : BaseResponse
{
    public List<GetRestaurantsByCategoryData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetRestaurantsByCategoryData
{
    public string RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public double Distance { get; set; }
    public string Coordinate { get; set; }
    public string Avatar { get; set; }
}