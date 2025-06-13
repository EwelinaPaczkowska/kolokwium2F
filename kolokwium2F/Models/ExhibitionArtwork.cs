using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class ExhibitionArtwork
{
    [Key]
    public int ExhibitionId { get; set; }
    [Required]
    public int ArtworkId { get; set; }
    public decimal InsuranceValue { get; set; }
}