using FullSolutionSoft.Application.Common;
using FullSolutionSoft.Application.DTOs;

namespace FullSolutionSoft.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(Guid customerId);

    Task<PagedResult<OrderDto>> GetFilteredAsync(OrderFilterDto filter);
}
