using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.DTOs;

public class GalleryDTO
{
    [Required]
    public int GalleryId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public DateTime EstablishedDate { get; set; }
}