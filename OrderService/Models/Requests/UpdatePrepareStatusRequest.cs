using OrderService.Enums;

namespace OrderService.Models.Requests;

public class UpdatePrepareStatusRequest
{
    public long OrderId { get; set; }
    public List<UpdatePrepareStatusInput> Input { get; set; } = new();
}

public class UpdatePrepareStatusInput
{
    public long OrderDetailId { get; set; }
    public PrepareStatus Status { get; set; }
}

