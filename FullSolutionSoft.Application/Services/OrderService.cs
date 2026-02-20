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
                CreatedAt = DateTime.UtcNow
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
    }
}
