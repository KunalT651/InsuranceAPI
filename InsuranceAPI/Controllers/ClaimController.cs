using InsuranceAPI.DTOs;
using InsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimDTO>>> GetClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimDTO>> GetClaim(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        [HttpGet("policy/{policyId}")]
        public async Task<ActionResult<IEnumerable<ClaimDTO>>> GetClaimsByPolicy(int policyId)
        {
            var claims = await _claimService.GetClaimsByPolicyIdAsync(policyId);
            return Ok(claims);
        }

        [HttpPost]
        public async Task<ActionResult<ClaimDTO>> CreateClaim(ClaimDTO claimDto)
        {
            var createdClaim = await _claimService.CreateClaimAsync(claimDto);
            if (createdClaim == null)
                return BadRequest("Invalid policy ID");

            return CreatedAtAction(nameof(GetClaim), new { id = createdClaim.Id }, createdClaim);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(int id, ClaimDTO claimDto)
        {
            var updatedClaim = await _claimService.UpdateClaimAsync(id, claimDto);
            if (updatedClaim == null)
                return NotFound();

            return Ok(updatedClaim);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateClaimStatus(int id, [FromBody] string status)
        {
            var updatedClaim = await _claimService.UpdateClaimStatusAsync(id, status);
            if (updatedClaim == null)
                return NotFound();

            return Ok(updatedClaim);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var result = await _claimService.DeleteClaimAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 