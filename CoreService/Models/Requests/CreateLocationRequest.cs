namespace CoreService.Models.Requests;

public class CreateLocationRequest
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Coordinate { get; set; }
}