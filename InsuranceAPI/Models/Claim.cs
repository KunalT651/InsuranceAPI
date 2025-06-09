using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAPI.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required]
        public DateTime ClaimDate { get; set; }

        public DateTime? ResolutionDate { get; set; }

        // Many Claims can belong to Many Policies (Many-to-Many relationship)
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    }
}