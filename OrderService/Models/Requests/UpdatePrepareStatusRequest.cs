using OrderService.Enums;

namespace OrderService.Models.Requests;

public class UpdatePrepareStatusRequest
{
    public int OrderId { get; set; }
    public List<UpdatePrepareStatusInput> Input { get; set; } = new();
}

public class UpdatePrepareStatusInput
{
    public string OrderDetailId { get; set; }
    public PrepareStatus Status { get; set; }
}

