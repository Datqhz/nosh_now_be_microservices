using Shared.Enums;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class UpdateEmployeeProfileResponse : BaseResponse
{
    public SystemRole PostProcessorData { get; set; }
}
