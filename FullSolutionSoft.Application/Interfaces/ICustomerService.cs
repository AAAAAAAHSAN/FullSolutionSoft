using FullSolutionSoft.Application.DTOs;

namespace FullSolutionSoft.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
    Task<CustomerDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<CustomerDto>> GetAllAsync();

    Task<CustomerDto?> UpdateAsync(Guid id, CreateCustomerDto dto);
    Task<bool> DeleteAsync(Guid id);
}
