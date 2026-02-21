using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FullSolutionSoft.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
        => _context = context;

    public async Task<Customer?> GetByIdAsync(Guid id)
        => await _context.Customers.FindAsync(id);

    public async Task<List<Customer>> GetAllAsync()
        => await _context.Customers.AsNoTracking().ToListAsync();

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
