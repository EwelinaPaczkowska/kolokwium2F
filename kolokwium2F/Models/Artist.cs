using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class Artist
{
    [Key]
    public int ArtistId { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
}