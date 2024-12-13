using Shared.Enums;
using Shared.Responses;

namespace CoreService.Models.Responses;

public class UpdateEmployeeProfileResponse : BaseResponse
{
    internal SystemRole PostProcessorData { get; set; }
}
