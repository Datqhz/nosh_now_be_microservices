namespace OrderService.Models.Requests;

public class UpdateVoucherRequest
{
    public long Id { get; set; }
    public string VoucherName { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Expired  { get; set; }
}
