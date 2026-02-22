using FullSolutionSoft.Domain.Enums;

namespace FullSolutionSoft.Application.DTOs;

public record CreateProductDto(
    string Name,
    string Description,
    ProductCategory ProductCategory
);

public record ProductDto(
    Guid Id,
     string Name,
     string Description,
     ProductCategory ProductCategory
);