using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        IQueryable<Order> GetQueryable();
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
        Task<int> CountAsync(IQueryable<Order> query);
        Task<List<Order>> ToListAsync(IQueryable<Order> query);

        Task SaveChangesAsync();

    }
}
