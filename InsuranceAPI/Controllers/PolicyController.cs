using InsuranceAPI.DTOs;
using InsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicyDTO>>> GetPolicies()
        {
            var policies = await _policyService.GetAllPoliciesAsync();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PolicyDTO>> GetPolicy(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null)
                return NotFound();

            return Ok(policy);
        }

        [HttpPost]
        public async Task<ActionResult<PolicyDTO>> CreatePolicy(PolicyDTO policyDto)
        {
            var createdPolicy = await _policyService.CreatePolicyAsync(policyDto);
            return CreatedAtAction(nameof(GetPolicy), new { id = createdPolicy.Id }, createdPolicy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(int id, PolicyDTO policyDto)
        {
            var updatedPolicy = await _policyService.UpdatePolicyAsync(id, policyDto);
            if (updatedPolicy == null)
                return NotFound();

            return Ok(updatedPolicy);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var result = await _policyService.DeletePolicyAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 