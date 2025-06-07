using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public string Contact { get; set; }

    [Required]
    [MaxLength(255)]
    public string Address { get; set; }

    // Ensure a single foreign key reference to Policy
    [ForeignKey(nameof(Policy))]
    public int PolicyId { get; set; }
    public Policy Policy { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}