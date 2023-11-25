using NSE.Core.Data;

namespace NSE.Order.Domain.Vouchers;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> GetVoucherByCode(string code);
    void Update(Voucher voucher);
}
