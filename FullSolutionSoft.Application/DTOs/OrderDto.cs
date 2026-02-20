using FullSolutionSoft.Domain.Enums;

namespace FullSolutionSoft.Application.DTOs;

public record OrderDto(
Guid Id,
Guid CustomerId,
string OrderNumber,
decimal TotalAmount,
OrderStatus Status,
DateTime CreatedAt
);

public record CreateOrderDto(
    Guid CustomerId,
    string OrderNumber,
    decimal TotalAmount
);

public record OrderFilterDto(
DateTime? FromDate,
DateTime? ToDate,
OrderStatus? Status,
int PageNumber = 1,
int PageSize = 10
);
