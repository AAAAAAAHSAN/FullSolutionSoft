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
        Task<(List<Order>, int totalCount)> GetFilteredAsync(
            DateTime? from,
            DateTime? to,
            OrderStatus? status,
            int pageNumber,
            int pageSize);
    }
}
