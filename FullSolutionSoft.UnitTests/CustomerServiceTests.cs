using FluentAssertions;
using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Application.Services;
using FullSolutionSoft.Domain.Entities;
using Moq;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repoMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _repoMock = new Mock<ICustomerRepository>();
        _service = new CustomerService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Customer()
    {
        // Arrange
        var dto = new CreateCustomerDto(
            "ahsan@test.com",
            "Ahsan",
            "Ullah"
        );

        _repoMock.Setup(x => x.AddAsync(It.IsAny<Customer>()))
                 .Returns(Task.CompletedTask);

        _repoMock.Setup(x => x.SaveChangesAsync())
                 .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be("ahsan@test.com");

        _repoMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
        _repoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}