using Shared.Responses;

namespace CoreService.Models.Responses;

public class GetCategoriesResponse : BaseResponse
{
    public List<GetCategoriesData> Data { get; set; }
}

public class GetCategoriesData
{
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryImage { get; set; }
}