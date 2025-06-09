using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

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
    }
} 