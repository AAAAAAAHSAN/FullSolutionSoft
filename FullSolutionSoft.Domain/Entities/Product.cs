using FullSolutionSoft.Domain.Enums;

namespace FullSolutionSoft.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductCategory ProductCategory { get; set; }
}
