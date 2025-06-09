using InsuranceAPI.DTOs;

namespace InsuranceAPI.Services.Interfaces
{
    public interface IClaimService
    {
        Task<IEnumerable<ClaimDTO>> GetAllClaimsAsync();
        Task<ClaimDTO> GetClaimByIdAsync(int id);
        Task<ClaimDTO> CreateClaimAsync(ClaimDTO claimDto);
        Task<ClaimDTO> UpdateClaimAsync(int id, ClaimDTO claimDto);
        Task<bool> DeleteClaimAsync(int id);
        Task<IEnumerable<ClaimDTO>> GetClaimsByPolicyIdAsync(int policyId);
        Task<ClaimDTO> UpdateClaimStatusAsync(int id, string status);
    }
} 