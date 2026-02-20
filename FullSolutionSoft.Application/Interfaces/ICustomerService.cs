using FullSolutionSoft.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
        Task<CustomerDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
    }
}
