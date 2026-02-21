using FullSolutionSoft.Domain.Enums;

namespace FullSolutionSoft.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OrderNumber { get; set; } = default!;

    public Customer Customer { get; set; } = null!;
}
