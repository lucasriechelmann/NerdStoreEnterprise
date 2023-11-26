using NSE.Order.Domain.Vouchers;

namespace NSE.Order.API.Application.DTO;
public class VoucherDTO
{
    public decimal? Percent { get; set; }
    public decimal? Value { get; set; }
    public string Code { get; set; }
    public int DiscountType { get; set; }
    public static explicit operator VoucherDTO(Voucher voucher) => new VoucherDTO
    {
        Code = voucher.Code,
        DiscountType = (int)voucher.TypeDiscount,
        Percent = voucher.Percent,
        Value = voucher.Value
    };
}
