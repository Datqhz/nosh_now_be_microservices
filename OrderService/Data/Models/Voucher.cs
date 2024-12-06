using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Data.Models;

public class Voucher
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string VoucherName { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Expired  { get; set; }
    public bool IsDeleted { get; set; }
    public virtual List<NoshPointTransaction> Transaction { get; set; }
}
