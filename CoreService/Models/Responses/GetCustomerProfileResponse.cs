using Shared.Enums;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetCustomerProfileResponse : BaseResponse
{
    public GetCustomerProfileData Data { get; set; }
}

public class GetCustomerProfileData
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Avatar { get; set; }
}