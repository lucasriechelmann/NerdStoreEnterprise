using NSE.Order.API.Application.DTO;

namespace NSE.Order.API.Application.Queries;

public interface IVoucherQueries
{
    Task<VoucherDTO> GetVoucherByCode(string code);
}
