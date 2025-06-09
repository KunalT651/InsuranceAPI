using InsuranceAPI.DTOs;
using InsuranceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByCustomer(int customerId)
        {
            var payments = await _paymentService.GetPaymentsByCustomerIdAsync(customerId);
            return Ok(payments);
        }

        [HttpGet("policy/{policyId}")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByPolicy(int policyId)
        {
            var payments = await _paymentService.GetPaymentsByPolicyIdAsync(policyId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO paymentDto)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.Id }, createdPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDTO paymentDto)
        {
            var updatedPayment = await _paymentService.UpdatePaymentAsync(id, paymentDto);
            if (updatedPayment == null)
                return NotFound();

            return Ok(updatedPayment);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromBody] string status)
        {
            var updatedPayment = await _paymentService.UpdatePaymentStatusAsync(id, status);
            if (updatedPayment == null)
                return NotFound();

            return Ok(updatedPayment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 