namespace NSE.BFF.Shopping.Models;
public class BasketDTO
{
    public decimal TotalValue { get; set; }
    public VoucherDTO Voucher { get; set; }
    public bool VoucherUsed { get; set; }
    public decimal Discount { get; set; }
    public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
}
