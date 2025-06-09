using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Models;
using InsuranceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly InsuranceDbContext _context;

        public CustomerService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDTO
                {
                    Id = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    DateOfBirth = c.DateOfBirth
                })
                .ToListAsync();
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDTO
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth
            };
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
                DateOfBirth = customerDto.DateOfBirth
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customerDto.Id = customer.CustomerId;
            return customerDto;
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(int id, CustomerDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.Phone = customerDto.Phone;
            customer.Address = customerDto.Address;
            customer.DateOfBirth = customerDto.DateOfBirth;

            await _context.SaveChangesAsync();
            return customerDto;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PolicyDTO>> GetCustomerPoliciesAsync(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Policies)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null) return null;

            return customer.Policies.Select(p => new PolicyDTO
            {
                Id = p.PolicyId,
                Type = p.Type,
                Coverage = p.Coverage,
                Premium = p.Premium,
                Duration = p.Duration,
                Status = p.Status,
                EndDate = p.EndDate
            });
        }
    }
} 