using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAPI.Models
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public decimal Coverage { get; set; }

        [Required]
        public decimal Premium { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Navigation properties
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}