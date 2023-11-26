using NSE.Order.API.Application.DTO;
using NSE.Order.Domain.Vouchers;

namespace NSE.Order.API.Application.Queries;
public class VoucherQueries : IVoucherQueries
{
    private readonly IVoucherRepository _voucherRepository;

    public VoucherQueries(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<VoucherDTO> GetVoucherByCode(string code)
    {
        var voucher = await _voucherRepository.GetVoucherByCode(code);

        if(voucher == null || !voucher.IsItValidForUse()) 
            return null;

        return (VoucherDTO)voucher;
    }
}
