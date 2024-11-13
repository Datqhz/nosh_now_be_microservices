namespace CoreService.Models.Requests;

public class UpdateLocationRequest
{
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public string Coordinate { get; set; }
    public string Phone { get; set; }
}