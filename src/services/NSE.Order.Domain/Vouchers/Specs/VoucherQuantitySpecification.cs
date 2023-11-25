using NetDevPack.Specification;
using System.Linq.Expressions;

namespace NSE.Order.Domain.Vouchers.Specs;

public class VoucherQuantitySpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression()
    {
        return voucher => voucher.Quantity > 0;
    }
}
