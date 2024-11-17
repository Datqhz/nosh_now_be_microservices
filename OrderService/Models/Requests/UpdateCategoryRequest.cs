namespace OrderService.Models.Requests;

public class UpdateCategoryRequest
{
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Image { get; set; }
}