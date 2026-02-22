using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FullSolutionSoft.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
    }

    public void Delete(Product entity)
    {
        _context.Products.Remove(entity);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async void Update(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }
}
