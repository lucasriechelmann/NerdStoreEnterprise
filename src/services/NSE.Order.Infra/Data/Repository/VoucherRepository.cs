using NSE.Core.Data;
using NSE.Order.Domain.Vouchers;
using System.Data;

namespace NSE.Order.Infra.Data.Repository;
public class VoucherRepository : IVoucherRepository
{
    private readonly OrderContext _context;

    public VoucherRepository(OrderContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<Voucher> GetVoucherByCode(string code)
    {
        throw new NotImplementedException();
    }

    public void Update(Voucher voucher)
    {
        throw new NotImplementedException();
    }
}
