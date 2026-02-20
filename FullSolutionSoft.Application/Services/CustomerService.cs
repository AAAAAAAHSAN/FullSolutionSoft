using FullSolutionSoft.Application.DTOs;
using FullSolutionSoft.Application.Interfaces;
using FullSolutionSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedAt = DateTime.Now
            };

            await _repository.AddAsync(customer);
            await _repository.SaveChangesAsync();

            return new CustomerDto(customer.Id, customer.Email, customer.FirstName, customer.LastName);
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto(customer.Id, customer.Email, customer.FirstName, customer.LastName);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(c => new CustomerDto(c.Id, c.Email, c.FirstName, c.LastName));
        }
    }
}
