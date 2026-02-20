using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullSolutionSoft.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service) // ← interface
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
