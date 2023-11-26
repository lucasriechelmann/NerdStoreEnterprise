using NSE.Core.DomainObjects;
using NSE.Order.Domain.Vouchers;

namespace NSE.Order.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    public Order(Guid customerId, decimal totalValue, List<OrderItem> orderItems,
            bool voucherUsed = false, decimal discount = 0, Guid? voucherId = null)
    {
        CustomerId = customerId;
        TotalValue = totalValue;
        _orderItems = orderItems;

        Discount = discount;
        VoucherUsed = voucherUsed;
        VoucherId = voucherId;
    }

    // EF ctor
    protected Order() { }

    public int Code { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTime RegisterDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Address Address { get; private set; }

    // EF Rel.
    public Voucher Voucher { get; private set; }

    public void AutorizarPedido() => Status = OrderStatus.Authorized;
    public void ApplyVoucher(Voucher voucher)
    {
        VoucherUsed = true;
        VoucherId = voucher.Id;
        Voucher = voucher;
    }
    public void ApplyAddress(Address address) => Address = address;
    public void CalculateOrderValue()
    {
        TotalValue = OrderItems.Sum(p => p.CalculateValue());
        CalculateDiscountTotalValue();
    }

    public void CalculateDiscountTotalValue()
    {
        if (!VoucherUsed) return;

        decimal desconto = 0;
        var value = TotalValue;

        if (Voucher.TypeDiscount == VoucherTypeDiscount.Percent)
        {
            if (Voucher.Percent.HasValue)
            {
                desconto = (value * Voucher.Percent.Value) / 100;
                value -= desconto;
            }
        }
        else
        {
            if (Voucher.Value.HasValue)
            {
                desconto = Voucher.Value.Value;
                value -= desconto;
            }
        }

        TotalValue = value < 0 ? 0 : value;
        Discount = desconto;
    }
}
