using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetEmployeeProfileResponse : BaseResponse
{
    public GetEmployeeProfileData Data { get; set; }
}

public class GetEmployeeProfileData
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Avatar { get; set; }
}