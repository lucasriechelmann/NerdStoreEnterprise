namespace NSE.Basket.API.Models;
public class Voucher
{
    public decimal? Percent { get; set; }
    public decimal? Value { get; set; }
    public string Code { get; set; }
    public DiscountTypeVoucher DiscountType { get; set; }
}
