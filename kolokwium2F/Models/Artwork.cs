using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class Artwork
{
    [Key]
    public int ArtworkId { get; set; }
    [Required]
    public int ArtistId { get; set; }
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public int YearCreate { get; set; }
}