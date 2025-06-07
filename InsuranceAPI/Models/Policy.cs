using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    // One Policy can have many Claims (Many-to-Many)
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
}