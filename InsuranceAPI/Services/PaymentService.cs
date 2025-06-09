using InsuranceAPI.Data;
using InsuranceAPI.DTOs;
using InsuranceAPI.Models;
using InsuranceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly InsuranceDbContext _context;

        public PaymentService(InsuranceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentDTO
                {
                    Id = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Status = p.Status,
                    CustomerId = p.CustomerId,
                    PolicyId = p.PolicyId
                })
                .ToListAsync();
        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            return new PaymentDTO
            {
                Id = payment.PaymentId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                Status = payment.Status,
                CustomerId = payment.CustomerId,
                PolicyId = payment.PolicyId
            };
        }

        public async Task<PaymentDTO> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod,
                TransactionId = paymentDto.TransactionId,
                Status = paymentDto.Status,
                CustomerId = paymentDto.CustomerId,
                PolicyId = paymentDto.PolicyId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            paymentDto.Id = payment.PaymentId;
            return paymentDto;
        }

        public async Task<PaymentDTO> UpdatePaymentAsync(int id, PaymentDTO paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            payment.Amount = paymentDto.Amount;
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.PaymentMethod = paymentDto.PaymentMethod;
            payment.TransactionId = paymentDto.TransactionId;
            payment.Status = paymentDto.Status;
            payment.CustomerId = paymentDto.CustomerId;
            payment.PolicyId = paymentDto.PolicyId;

            await _context.SaveChangesAsync();
            return paymentDto;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByCustomerIdAsync(int customerId)
        {
            return await _context.Payments
                .Where(p => p.CustomerId == customerId)
                .Select(p => new PaymentDTO
                {
                    Id = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Status = p.Status,
                    CustomerId = p.CustomerId,
                    PolicyId = p.PolicyId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByPolicyIdAsync(int policyId)
        {
            return await _context.Payments
                .Where(p => p.PolicyId == policyId)
                .Select(p => new PaymentDTO
                {
                    Id = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionId = p.TransactionId,
                    Status = p.Status,
                    CustomerId = p.CustomerId,
                    PolicyId = p.PolicyId
                })
                .ToListAsync();
        }

        public async Task<PaymentDTO> UpdatePaymentStatusAsync(int id, string status)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            payment.Status = status;
            await _context.SaveChangesAsync();

            return new PaymentDTO
            {
                Id = payment.PaymentId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                Status = payment.Status,
                CustomerId = payment.CustomerId,
                PolicyId = payment.PolicyId
            };
        }
    }
} 