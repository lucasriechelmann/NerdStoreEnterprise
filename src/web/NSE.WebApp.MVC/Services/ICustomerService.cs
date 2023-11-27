using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface ICustomerService
{
    Task<AddressViewModel> GetAddress();
    Task<ResponseResult> AddAddress(AddressViewModel address);
}
