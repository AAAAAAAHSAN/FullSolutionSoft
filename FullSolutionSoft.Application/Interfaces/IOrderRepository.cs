using FullSolutionSoft.Domain.Entities;

namespace FullSolutionSoft.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    IQueryable<Order> GetQueryable();
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    Task<int> CountAsync(IQueryable<Order> query);
    Task<List<Order>> ToListAsync(IQueryable<Order> query);

    Task SaveChangesAsync();

}
