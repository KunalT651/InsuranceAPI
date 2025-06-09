using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

// Checks if the input customer object is valid.
// Saves new customer into the database.
// Returns Created (201) response with customer details.

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        if (customer == null)
        {
            return BadRequest("Customer data is required.");
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
    }

 //  Finds the customer by ID in the database.
 //Updates the fields based on user input.
 //Saves changes and returns a 204 No Content response.

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, Customer updatedCustomer)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        customer.Name = updatedCustomer.Name;
        customer.Contact = updatedCustomer.Contact;
        customer.Address = updatedCustomer.Address;
        customer.PolicyId = updatedCustomer.PolicyId;

        await _context.SaveChangesAsync();
        return NoContent(); // 204 Response (successful update)
    }

    // Finds the customer by ID in the database.
// Deletes the customer entry from the table.
// Saves changes and returns 204 No Content response(successful deletion).

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 Response (successful deletion)
    }
}