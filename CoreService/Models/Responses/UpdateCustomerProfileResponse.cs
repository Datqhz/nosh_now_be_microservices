using Shared.Responses;

namespace CoreService.Models.Responses;

public class UpdateCustomerProfileResponse : BaseResponse
{
    public UpdateCustomerProfilePostProcessorData PostProcessorData { get; set; }
}

public class UpdateCustomerProfilePostProcessorData
{
    public string CustomerId { get; set; }
    public string DisplayName { get; set; }
    public string Avatar { get; set; }
}
