using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAPI.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

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

        // Foreign keys
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("PolicyId")]
        public Policy Policy { get; set; }
    }
}