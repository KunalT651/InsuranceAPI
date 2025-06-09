using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Navigation properties
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}