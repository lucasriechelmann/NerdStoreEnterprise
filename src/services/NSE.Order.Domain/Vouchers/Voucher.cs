using NSE.Core.DomainObjects;
using NSE.Order.Domain.Vouchers.Specs;

namespace NSE.Order.Domain.Vouchers;
public class Voucher : Entity, IAggregateRoot
{
    public string Code { get; private set; }
    public decimal? Percent { get; private set; }
    public decimal? Value { get; private set; }
    public int Quantity { get; private set; }
    public VoucherTypeDiscount TypeDiscount { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? UsedDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool Active { get; private set; }
    public bool Used { get; private set; }

    public bool IsItValidForUse()
    {
        return new VoucherActiveSpecification()
            .And(new VoucherDataSpecification())
            .And(new VoucherQuantitySpecification())
            .IsSatisfiedBy(this);
    }

    public void MarkAsUsed()
    {
        Active = false;
        Used = true;
        Quantity = 0;
        UsedDate = DateTime.Now;
    }

    public void DebitQuantity()
    {
        Quantity -= 1;
        if (Quantity >= 1) return;

        MarkAsUsed();
    }
}
