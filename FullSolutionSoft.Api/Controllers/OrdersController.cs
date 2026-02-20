using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FullSolutionSoft.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByCustomer), new { customerId = result.CustomerId }, result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(Guid customerId)
        {
            var orders = await _service.GetOrdersByCustomerAsync(customerId);
            return Ok(orders);
        }
    }
}
