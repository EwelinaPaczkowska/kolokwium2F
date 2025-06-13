using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class Gallery
{
    [Key]
    public int GalleryId { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public DateTime EstablishedDate { get; set; }
    public ICollection<Exhibition> Artworks { get; set; }
}