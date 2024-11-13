using Shared.Responses;

namespace CoreService.Models.Responses;

public class UpdateRestaurantProfileResponse : BaseResponse
{
    public UpdateRestaurantProfilePostProcessorData PostProcessorData { get; set; }
}

public class UpdateRestaurantProfilePostProcessorData
{
    public string Id { get; init; }
    public string DisplayName { get; init; }
    public string Coordinate { get; init; }
    public string Phone { get; init; }
    public string Avatar { get; init; }
}
