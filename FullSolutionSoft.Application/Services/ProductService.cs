using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;

namespace FullSolutionSoft.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            ProductCategory = dto.ProductCategory,
        };

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        return new ProductDto(product.Id, product.Name, product.Description, product.ProductCategory);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return false;

        _repository.Delete(product); // We need to add Remove in repository
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<ProductDto> GetProductByIdsync(Guid Id)
    {
        var product = await _repository.GetByIdAsync(Id);
        if (product == null) return null;

        return new ProductDto(product.Id, product.Name, product.Description, product.ProductCategory);
    }

    public Task<IEnumerable<ProductDto>> GetProductByOrderAsync(Guid OrderId)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto?> UpdateAsync(Guid id, CreateProductDto dto)
    {
        throw new NotImplementedException();
    }
}
