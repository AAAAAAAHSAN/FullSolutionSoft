using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Domain.Enums;
using FullSolutionSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public async Task<(List<Order>, int)> GetFilteredAsync(
    DateTime? from,
    DateTime? to,
    OrderStatus? status,
    int pageNumber,
    int pageSize)
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
    }
}
