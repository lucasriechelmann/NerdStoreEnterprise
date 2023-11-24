namespace NSE.BFF.Shopping.Models;
public class VoucherDTO
{
    public decimal? Percent { get; set; }
    public decimal? Value { get; set; }
    public string Code { get; set; }
    public int DiscountType { get; set; }
}
