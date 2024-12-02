using Shared.Enums;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetEmployeeByIdResponse : BaseResponse
{
    public GetEmployeeByIdData Data { get; set; }
}

public class GetEmployeeByIdData
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public RestaurantRole Role { get; set; }
}