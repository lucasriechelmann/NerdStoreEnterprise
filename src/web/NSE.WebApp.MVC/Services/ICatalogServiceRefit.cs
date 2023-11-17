using NSE.WebApp.MVC.Models;
using Refit;

namespace NSE.WebApp.MVC.Services;

public interface ICatalogServiceRefit
{
    [Get("/catalog/products/")]
    Task<IEnumerable<ProductViewModel>> GetAll();
    [Get("/catalog/products/{id}")]
    Task<ProductViewModel> GetById(Guid id);
}
