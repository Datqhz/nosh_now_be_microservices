using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace OrderService.Data.Models;

public class NoshPointTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }
    public NoshPointTransactionType TransactionType { get; set; }
    public long OrderId { get; set; }
    public string CustomerId { get; set; }
    public long? VoucherId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Voucher Voucher { get; set; }
}
