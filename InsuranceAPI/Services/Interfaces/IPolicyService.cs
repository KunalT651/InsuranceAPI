using InsuranceAPI.DTOs;
using InsuranceAPI.Models;

namespace InsuranceAPI.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<IEnumerable<PolicyDTO>> GetAllPoliciesAsync();
        Task<PolicyDTO> GetPolicyByIdAsync(int id);
        Task<PolicyDTO> CreatePolicyAsync(PolicyDTO policyDto);
        Task<PolicyDTO> UpdatePolicyAsync(int id, PolicyDTO policyDto);
        Task<bool> DeletePolicyAsync(int id);
    }
} 