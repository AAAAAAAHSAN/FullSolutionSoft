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
                CreatedAt = DateTime.Now,
                UpdateddAt = DateTime.Now
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

        public async Task<CustomerDto?> UpdateAsync(Guid id, CreateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return null;

            // Update only non-null fields
            if (!string.IsNullOrEmpty(dto.Email)) customer.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.FirstName)) customer.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) customer.LastName = dto.LastName;
            customer.UpdateddAt = DateTime.Now;

            await _repository.SaveChangesAsync();

            return new CustomerDto(customer.Id, customer.Email, customer.FirstName, customer.LastName);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return false;

            await _repository.DeleteAsync(customer); // We need to add Remove in repository
            await _repository.SaveChangesAsync();
            return true;
        }

   
    }
}
