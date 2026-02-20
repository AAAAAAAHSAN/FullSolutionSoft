using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Domain.Enums;
using FullSolutionSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FullSolutionSoft.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<(List<Order>, int)> GetFilteredAsync(DateTime? from, DateTime? to, OrderStatus? status, int pageNumber, int pageSize)
        {
            var query = _context.Orders.AsQueryable();

            if (from.HasValue)
                query = query.Where(o => o.CreatedAt >= from.Value);

            if (to.HasValue)
                query = query.Where(o => o.CreatedAt <= to.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId) =>
            await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}
