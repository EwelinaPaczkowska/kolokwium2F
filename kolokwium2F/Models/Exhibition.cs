using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class Exhibition
{
    [Key]
    public int ExhibitionId { get; set; }
    public int GalleryId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    public int NumberOfArtworks { get; set; }
    public Artwork Artwork { get; set; }
}