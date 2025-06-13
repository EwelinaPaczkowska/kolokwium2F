using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.DTOs;

public class GalleryInsertDTO
{
    [Required]
    public GalleryDTO Gallery { get; set; }
    //[Required]
    //public List<PurchaseInsertDTO> Purchases { get; set; }
}