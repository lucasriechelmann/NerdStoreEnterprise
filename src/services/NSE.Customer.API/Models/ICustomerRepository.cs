using NSE.Core.Data;

namespace NSE.Customer.API.Models;
public interface ICustomerRepository : IRepository<Customer>
{
    void Add(Models.Customer customer);
    Task<IEnumerable<Models.Customer>> GetAll();
    Task<Models.Customer> GetByCpf(string cpf);
}
