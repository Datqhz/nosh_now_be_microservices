namespace OrderService.Data.Models;

public class Customer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int BoomCount { get; set; }
    public decimal NoshPoint { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<NoshPointTransaction> Transactions { get; set; }
    public virtual IEnumerable<VoucherWallet> VoucherWallets { get; set; }
}