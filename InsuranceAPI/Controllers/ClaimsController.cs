using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;

[Route("api/[controller]")]
[ApiController]
public class ClaimsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClaimsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/claims
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Claim>>> GetClaims()
    {
        return await _context.Claims.ToListAsync();
    }

    // Validates the input before saving.
    // Adds a new claim to the database.
    // Returns a 201 Created response with the newly added claim.
    [HttpPost]
    public async Task<ActionResult<Claim>> CreateClaim(Claim claim)
    {
        if (claim == null)
        {
            return BadRequest("Claim data is required.");
        }

        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClaims), new { id = claim.ClaimId }, claim);
    }

    // Finds the claim by ID in the database.
    // Updates the claim details based on user input.
    // Saves changes and returns a 204 No Content response for successful modification
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClaim(int id, Claim updatedClaim)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim == null)
        {
            return NotFound($"Claim with ID {id} not found.");
        }

        // Updating claim details
        claim.Status = updatedClaim.Status;
        claim.Amount = updatedClaim.Amount;
        claim.DateFiled = updatedClaim.DateFiled;
        claim.FraudFlag = updatedClaim.FraudFlag;

        await _context.SaveChangesAsync();
        return NoContent(); // 204 Response (successful update)
    }

    //✔ Finds the claim by ID in the database.
// Removes the claim entry from the table.
// Saves changes and returns a 204 No Content response(successful deletion).
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);
        if (claim == null)
        {
            return NotFound($"Claim with ID {id} not found.");
        }

        _context.Claims.Remove(claim);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 Response (successful deletion)
    }
}