using FullSolutionSoft.Application.DTOs;

namespace FullSolutionSoft.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto dto);
    Task<IEnumerable<ProductDto>> GetProductByOrderAsync(Guid OrderId);
    Task<ProductDto> GetProductByIdsync(Guid Id);
    Task<CustomerDto?> UpdateAsync(Guid id, CreateProductDto dto);
    Task<bool> DeleteAsync(Guid id);

}
