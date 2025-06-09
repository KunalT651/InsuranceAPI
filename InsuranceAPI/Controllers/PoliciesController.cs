using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[Route("api/[controller]")]
[ApiController]
public class PoliciesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PoliciesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/policies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies()
    {
        return await _context.Policies.ToListAsync();
    }

    //✔ Validates the input before saving.
    //Adds a new policy to the database.
    //Returns a 201 Created response with the newly added policy.
    [HttpPost]
    public async Task<ActionResult<Policy>> CreatePolicy(Policy policy)
    {
        if (policy == null)
        {
            return BadRequest("Policy data is required.");
        }

        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPolicies), new { id = policy.PolicyId }, policy);
    }

    // Finds the policy by ID in the database.
    // Updates policy details based on user input.
    // Saves changes and returns a 204 No Content response for successful modification
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePolicy(int id, Policy updatedPolicy)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
        {
            return NotFound($"Policy with ID {id} not found.");
        }

        // Match field names with the Policy model
        policy.Type = updatedPolicy.Type;
        policy.Coverage = updatedPolicy.Coverage;
        policy.Premium = updatedPolicy.Premium;
        policy.Duration = updatedPolicy.Duration;
        policy.Status = updatedPolicy.Status;
        policy.EndDate = updatedPolicy.EndDate;

        await _context.SaveChangesAsync();
        return NoContent(); // 204 Response (successful update)
    }

    // Finds the policy by ID in the database.
    // Removes the policy entry from the table.
    // Saves changes and returns a 204 No Content response(successful deletion).
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePolicy(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
        {
            return NotFound($"Policy with ID {id} not found.");
        }

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();

        return NoContent(); // 204 Response (successful deletion)
    }
}