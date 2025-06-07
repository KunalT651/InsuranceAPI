using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Claim
{
    [Key]
    public int ClaimId { get; set; }

    [Required]
    public DateTime DateFiled { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Under Review";

    public bool FraudFlag { get; set; } = false;

    // Many Claims can belong to Many Policies (Many-to-Many relationship)
    public ICollection<Policy> Policies { get; set; } = new List<Policy>();
}