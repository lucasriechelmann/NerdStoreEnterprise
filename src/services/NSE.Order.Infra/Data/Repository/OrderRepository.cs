using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Order.Domain.Orders;
using System.Data.Common;

namespace NSE.Order.Infra.Data.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;
    public OrderRepository(OrderContext context)
    {
        _context = context;
    }
    public IUnitOfWork UnitOfWork => _context;
    public DbConnection GetConnection() => _context.Database.GetDbConnection();
    public void Add(Domain.Orders.Order order) => _context.Orders.Add(order);
    public void Update(Domain.Orders.Order order) => _context.Orders.Update(order);
    public async Task<Domain.Orders.Order> GetById(Guid id) => await _context.Orders.FindAsync(id);
    public async Task<IEnumerable<Domain.Orders.Order>> GetListByCustomerId(Guid customerId) =>
        await _context
            .Orders
            .Include(p => p.OrderItems)
            .AsNoTracking()
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();
    public async Task<OrderItem> GetItemById(Guid id) => await _context
        .OrderItems
        .FindAsync(id);
    public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId) => await _context
        .OrderItems
        .Where(x => x.OrderId == orderId && x.ProductId == productId)
        .FirstOrDefaultAsync();
    public async Task<Domain.Orders.Order> GetLastOrder(Guid customerId) => await _context
        .Orders
        .AsNoTracking()
        .Where(x => x.CustomerId == customerId && x.Status == OrderStatus.Authorized)
        .OrderByDescending(x => x.RegisterDate)
        .FirstOrDefaultAsync();       
    public void Dispose() => _context.Dispose();
}
