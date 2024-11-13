using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetSavedLocationByCustomerResponse : BaseResponse
{
    public List<GetSavedLocationByCustomerData> Data { get; set; }
}

public class GetSavedLocationByCustomerData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Coordinate { get; set; }
}