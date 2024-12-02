namespace CoreService.Models.Requests;

public class UpdateEmployeeProfileRequest
{
    public string EmployeeId { get; set; }
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
}
