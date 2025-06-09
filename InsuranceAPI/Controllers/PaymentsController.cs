using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PaymentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/payments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        return await _context.Payments.ToListAsync();
    }

    // Validates the input before saving.
    // Adds a new payment to the database.
    // Returns a 201 Created response with the newly added payment.

    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
    {
        if (payment == null)
        {
            return BadRequest("Payment data is required.");
        }

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPayments), new { id = payment.PaymentId }, payment);
    }

    // Finds the payment by ID in the database.
    // Updates payment details based on user input.
    // Saves changes and returns a 204 No Content response for successful modification

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePayment(int id, Payment updatedPayment)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        // Updating payment details
        payment.Amount = updatedPayment.Amount;
        payment.PaymentDate = updatedPayment.PaymentDate;
        payment.PaymentStatus = updatedPayment.PaymentStatus;

        await _context.SaveChangesAsync();
        return NoContent(); // 204 Response (successful update)
    }

    // Finds the payment by ID in the database.
    // Removes the payment entry from the table.
    // Saves changes and returns a 204 No Content response(successful deletion).

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound($"Payment with ID {id} not found.");
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 Response (successful deletion)
    }
}