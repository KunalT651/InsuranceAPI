using InsuranceAPI.DTOs;
using InsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet("{id}/policies")]
        public async Task<ActionResult<IEnumerable<PolicyDTO>>> GetCustomerPolicies(int id)
        {
            var policies = await _customerService.GetCustomerPoliciesAsync(id);
            if (policies == null)
                return NotFound();

            return Ok(policies);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerDTO customerDto)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDTO customerDto)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);
            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 