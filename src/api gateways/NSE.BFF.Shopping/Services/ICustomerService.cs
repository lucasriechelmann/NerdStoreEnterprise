using NSE.BFF.Shopping.Models;

namespace NSE.BFF.Shopping.Services;
public interface ICustomerService
{
    Task<AddressDTO> GetAddress();
}
