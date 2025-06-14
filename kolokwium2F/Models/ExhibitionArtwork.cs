﻿using System.ComponentModel.DataAnnotations;

namespace kolokwium2F.Models;

public class ExhibitionArtwork
{
    [Key]
    public int ExhibitionId { get; set; }
    [Required]
    public int ArtworkId { get; set; }
    
    [Required]
    public int GalleryId { get; set; }
    public decimal InsuranceValue { get; set; }
    public Gallery Gallery { get; set; }
}