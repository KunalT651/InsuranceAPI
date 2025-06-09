using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Models;
using InsuranceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly InsuranceDbContext _context;

        public ClaimService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClaimDTO>> GetAllClaimsAsync()
        {
            return await _context.Claims
                .Select(c => new ClaimDTO
                {
                    Id = c.ClaimId,
                    Description = c.Description,
                    Amount = c.Amount,
                    Status = c.Status,
                    ClaimDate = c.ClaimDate,
                    ResolutionDate = c.ResolutionDate,
                    PolicyId = c.Policies.FirstOrDefault().PolicyId
                })
                .ToListAsync();
        }

        public async Task<ClaimDTO> GetClaimByIdAsync(int id)
        {
            var claim = await _context.Claims
                .Include(c => c.Policies)
                .FirstOrDefaultAsync(c => c.ClaimId == id);

            if (claim == null) return null;

            return new ClaimDTO
            {
                Id = claim.ClaimId,
                Description = claim.Description,
                Amount = claim.Amount,
                Status = claim.Status,
                ClaimDate = claim.ClaimDate,
                ResolutionDate = claim.ResolutionDate,
                PolicyId = claim.Policies.FirstOrDefault()?.PolicyId ?? 0
            };
        }

        public async Task<ClaimDTO> CreateClaimAsync(ClaimDTO claimDto)
        {
            var policy = await _context.Policies.FindAsync(claimDto.PolicyId);
            if (policy == null) return null;

            var claim = new Claim
            {
                Description = claimDto.Description,
                Amount = claimDto.Amount,
                Status = claimDto.Status,
                ClaimDate = claimDto.ClaimDate,
                ResolutionDate = claimDto.ResolutionDate,
                Policies = new List<Policy> { policy }
            };

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            claimDto.Id = claim.ClaimId;
            return claimDto;
        }

        public async Task<ClaimDTO> UpdateClaimAsync(int id, ClaimDTO claimDto)
        {
            var claim = await _context.Claims
                .Include(c => c.Policies)
                .FirstOrDefaultAsync(c => c.ClaimId == id);

            if (claim == null) return null;

            claim.Description = claimDto.Description;
            claim.Amount = claimDto.Amount;
            claim.Status = claimDto.Status;
            claim.ClaimDate = claimDto.ClaimDate;
            claim.ResolutionDate = claimDto.ResolutionDate;

            if (claimDto.PolicyId != 0)
            {
                var policy = await _context.Policies.FindAsync(claimDto.PolicyId);
                if (policy != null)
                {
                    claim.Policies.Clear();
                    claim.Policies.Add(policy);
                }
            }

            await _context.SaveChangesAsync();
            return claimDto;
        }

        public async Task<bool> DeleteClaimAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return false;

            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ClaimDTO>> GetClaimsByPolicyIdAsync(int policyId)
        {
            return await _context.Claims
                .Where(c => c.Policies.Any(p => p.PolicyId == policyId))
                .Select(c => new ClaimDTO
                {
                    Id = c.ClaimId,
                    Description = c.Description,
                    Amount = c.Amount,
                    Status = c.Status,
                    ClaimDate = c.ClaimDate,
                    ResolutionDate = c.ResolutionDate,
                    PolicyId = policyId
                })
                .ToListAsync();
        }

        public async Task<ClaimDTO> UpdateClaimStatusAsync(int id, string status)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return null;

            claim.Status = status;
            if (status == "Resolved")
            {
                claim.ResolutionDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return new ClaimDTO
            {
                Id = claim.ClaimId,
                Description = claim.Description,
                Amount = claim.Amount,
                Status = claim.Status,
                ClaimDate = claim.ClaimDate,
                ResolutionDate = claim.ResolutionDate,
                PolicyId = claim.Policies.FirstOrDefault()?.PolicyId ?? 0
            };
        }
    }
} 