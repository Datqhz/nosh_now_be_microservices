namespace Shared.Models.Dtos;

public class PagingDto
{
    public int PageNumber { get; set; }
    public int MaxPerPage { get; set; }
    public int TotalItem { get; set; }
    public int TotalPage { get; set; }
}