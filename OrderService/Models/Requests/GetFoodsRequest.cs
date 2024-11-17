namespace OrderService.Models.Requests;

public class GetFoodsRequest
{
    public string Keyword { get; set; } = String.Empty;
    public int PageNumber { get; set; } = 1;
    public int MaxPerPage { get; set; } = 10;
}