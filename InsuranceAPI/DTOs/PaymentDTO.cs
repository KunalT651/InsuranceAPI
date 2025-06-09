using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransactionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
    }
} 