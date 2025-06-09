using InsuranceAPI.DTOs;

namespace InsuranceAPI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync();
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task<PaymentDTO> CreatePaymentAsync(PaymentDTO paymentDto);
        Task<PaymentDTO> UpdatePaymentAsync(int id, PaymentDTO paymentDto);
        Task<bool> DeletePaymentAsync(int id);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByCustomerIdAsync(int customerId);
        Task<IEnumerable<PaymentDTO>> GetPaymentsByPolicyIdAsync(int policyId);
        Task<PaymentDTO> UpdatePaymentStatusAsync(int id, string status);
    }
} 