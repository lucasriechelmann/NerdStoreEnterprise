using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Data.Repository;
public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;
    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Models.Customer customer) =>
        _context.Customers.Add(customer);
    public async Task<IEnumerable<Models.Customer>> GetAll() =>
        await _context.Customers.AsNoTracking().ToListAsync();
    public async Task<Models.Customer> GetByCpf(string cpf) =>
        await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    public void AddAddress(Address address) => _context.Addresses.Add(address);
    public async Task<Address> GetAddressById(Guid id) =>
         await _context.Addresses.FirstOrDefaultAsync(e => e.CustomerId == id);
    public void Dispose() => _context.Dispose();
}
