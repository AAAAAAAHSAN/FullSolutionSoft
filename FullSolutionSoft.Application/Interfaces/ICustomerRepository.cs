using FullSolutionSoft.Domain.Entities;

namespace FullSolutionSoft.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<List<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    Task SaveChangesAsync();
}
