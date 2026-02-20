using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Application.DTOs
{
    public record OrderDto(
    Guid Id,
    Guid CustomerId,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt
);

    public record CreateOrderDto(
        Guid CustomerId,
        decimal TotalAmount
    );
}
