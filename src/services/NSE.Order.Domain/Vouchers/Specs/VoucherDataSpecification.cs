using NetDevPack.Specification;
using System.Linq.Expressions;

namespace NSE.Order.Domain.Vouchers.Specs;
public class VoucherDataSpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression()
    {
        return voucher => voucher.ExpirationDate >= DateTime.Now;
    }
}
