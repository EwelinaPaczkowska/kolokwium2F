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
    public int YearCreated { get; set; }

    public ExhibitionArtwork ExhibitionArtwork { get; set; }
    public int InsuranceValue { get; set; }
}