using FluentAssertions;
using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Application.Services;
using FullSolutionSoft.Domain.Entities;
using FullSolutionSoft.Domain.Enums;
using Moq;

namespace FullSolutionSoft.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _customerRepoMock = new Mock<ICustomerRepository>();

        _service = new OrderService(
            _orderRepoMock.Object,
            _customerRepoMock.Object
        );
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Order()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        _customerRepoMock.Setup(x => x.GetByIdAsync(customerId))
            .ReturnsAsync(new Customer
            {
                Id = customerId,
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            });

        _orderRepoMock.Setup(x => x.AddAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        _orderRepoMock.Setup(x => x.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var dto = new CreateOrderDto(
            customerId,
            "100",
            1200
        );

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        result.TotalAmount.Should().Be(1200);
        result.Status.Should().Be(OrderStatus.Pending);

        _orderRepoMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        _orderRepoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetFilteredAsync_Should_Return_Paged_Result()
    {
        // Arrange
        var orders = new List<Order>
    {
        new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 100,
            Status = OrderStatus.Completed,
            CreatedAt = DateTime.UtcNow
        },
        new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            TotalAmount = 200,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        }
    }.AsQueryable();

        _orderRepoMock.Setup(x => x.GetQueryable())
            .Returns(orders);

        _orderRepoMock.Setup(x => x.CountAsync(It.IsAny<IQueryable<Order>>()))
            .ReturnsAsync(2);

        _orderRepoMock.Setup(x => x.ToListAsync(It.IsAny<IQueryable<Order>>()))
            .ReturnsAsync(orders.ToList());

        var filter = new OrderFilterDto(
            null,
            null,
            null,
            1,
            10
        );

        // Act
        var result = await _service.GetFilteredAsync(filter);

        // Assert
        result.TotalCount.Should().Be(2);
        result.Items.Should().HaveCount(2);
    }
}