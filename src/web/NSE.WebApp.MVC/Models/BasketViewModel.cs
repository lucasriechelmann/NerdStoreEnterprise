namespace NSE.WebApp.MVC.Models;

public class BasketViewModel
{
    public decimal TotalValue { get; set; }
    public VoucherViewModel Voucher { get; set; }
    public bool VoucherUsed { get; set; }
    public decimal Discount { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
}
