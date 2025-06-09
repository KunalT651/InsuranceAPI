using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Models;
using InsuranceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly InsuranceDbContext _context;

        public PolicyService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolicyDTO>> GetAllPoliciesAsync()
        {
            return await _context.Policies
                .Select(p => new PolicyDTO
                {
                    Id = p.PolicyId,
                    Type = p.Type,
                    Coverage = p.Coverage,
                    Premium = p.Premium,
                    Duration = p.Duration,
                    Status = p.Status,
                    EndDate = p.EndDate
                })
                .ToListAsync();
        }

        public async Task<PolicyDTO> GetPolicyByIdAsync(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null) return null;

            return new PolicyDTO
            {
                Id = policy.PolicyId,
                Type = policy.Type,
                Coverage = policy.Coverage,
                Premium = policy.Premium,
                Duration = policy.Duration,
                Status = policy.Status,
                EndDate = policy.EndDate
            };
        }

        public async Task<PolicyDTO> CreatePolicyAsync(PolicyDTO policyDto)
        {
            var policy = new Policy
            {
                Type = policyDto.Type,
                Coverage = policyDto.Coverage,
                Premium = policyDto.Premium,
                Duration = policyDto.Duration,
                Status = policyDto.Status,
                EndDate = policyDto.EndDate
            };

            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();

            policyDto.Id = policy.PolicyId;
            return policyDto;
        }

        public async Task<PolicyDTO> UpdatePolicyAsync(int id, PolicyDTO policyDto)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null) return null;

            policy.Type = policyDto.Type;
            policy.Coverage = policyDto.Coverage;
            policy.Premium = policyDto.Premium;
            policy.Duration = policyDto.Duration;
            policy.Status = policyDto.Status;
            policy.EndDate = policyDto.EndDate;

            await _context.SaveChangesAsync();
            return policyDto;
        }

        public async Task<bool> DeletePolicyAsync(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null) return false;

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 