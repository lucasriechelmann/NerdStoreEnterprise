namespace NSE.Order.API.Application.DTO;
public class OrderDTO
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public int Status { get; set; }
    public DateTime Date { get; set; }  
    public decimal TotalValue { get; set; }
    public decimal Discount { get; set; }   
    public string VoucherCode { get; set; }
    public bool VoucherUsed { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
    public AddressDTO Address { get; set; }
    public static explicit operator OrderDTO(Domain.Orders.Order order) =>
        new OrderDTO
        {
            Id = order.Id,
            Code = order.Code,
            Status = (int)order.Status,
            Date = order.RegisterDate,
            TotalValue = order.TotalValue,
            Discount = order.Discount,
            VoucherCode = order.Voucher?.Code,
            VoucherUsed = order.VoucherUsed,
            OrderItems = order.OrderItems.Select(item => (OrderItemDTO)item).ToList(),
            Address = (AddressDTO)order.Address
        };
}
