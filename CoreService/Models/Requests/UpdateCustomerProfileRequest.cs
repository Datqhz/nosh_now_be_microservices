namespace CoreService.Models.Requests;

public class UpdateCustomerProfileRequest
{
    public string DisplayName { get; set; }
    public string PhoneNumber { get; set; }
    public string Avatar { get; set; }
}