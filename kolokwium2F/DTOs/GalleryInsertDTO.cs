using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.DTOs;

public class GalleryInsertDTO
{
    [Required]
    public GalleryDTO Gallery { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    public List<ArtworkDTO> Artworks { get; set; }
}