using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs
{
    public class PolicyDTO
    {
        public int Id { get; set; }

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
    }
} 