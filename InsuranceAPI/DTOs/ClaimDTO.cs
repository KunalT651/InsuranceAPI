using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs
{
    public class ClaimDTO
    {
        public int Id { get; set; }

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

        public int PolicyId { get; set; }
    }
} 