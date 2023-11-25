using NSE.BFF.Shopping.Models;

namespace NSE.BFF.Shopping.Services;
public interface ICatalogService
{
    Task<ProductDTO> GetById(Guid id);
    Task<IEnumerable<ProductDTO>> GetItems(IEnumerable<Guid> ids);
}
