using FullSolutionSoft.Application.Common;
using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Domain.Enums;

namespace FullSolutionSoft.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            // Verify customer exists
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                OrderNumber = dto.OrderNumber,
                TotalAmount = dto.TotalAmount,
                CreatedAt = DateTime.Now
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return new OrderDto(order.Id, order.CustomerId, order.OrderNumber, order.TotalAmount,OrderStatus.Pending, order.CreatedAt);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerAsync(Guid customerId)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            return orders.Select(o => new OrderDto(o.Id, o.CustomerId, o.OrderNumber, o.TotalAmount,o.Status, o.CreatedAt));
        }

        public async Task<PagedResult<OrderDto>> GetFilteredAsync(OrderFilterDto filter)
        {
            var query = _orderRepository.GetQueryable();

            if (filter.FromDate.HasValue)
                query = query.Where(o => o.CreatedAt >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.CreatedAt <= filter.ToDate.Value);

            if (filter.Status.HasValue)
            {
                query = query.Where(o => o.Status == filter.Status.Value);
            }

            var totalCount = await _orderRepository.CountAsync(query);

            query = query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var orders = await _orderRepository.ToListAsync(query);

            var dtoList = orders.Select(o => new OrderDto(
                o.Id,
                o.CustomerId,
                o.OrderNumber,
                o.TotalAmount,
                o.Status,
                o.CreatedAt
            ));

            return new PagedResult<OrderDto>(
                dtoList,
                totalCount,
                filter.PageNumber,
                filter.PageSize
            );
        }
    }
}
