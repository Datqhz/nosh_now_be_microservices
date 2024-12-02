using Shared.Enums;
using Shared.Models.Dtos;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetEmployeesResponse : BaseResponse
{
    public List<GetEmployeesData> Data { get; set; }
    public PagingDto Paging { get; set; }
}

public class GetEmployeesData
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
    public RestaurantRole Role { get; set; }
}