using kolokwium2F.DTOs;
using kolokwium2F.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium2F.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GalleriesController : ControllerBase
{
    private readonly IGalleriesService _galleriesService;
    
    public GalleriesController(IGalleriesService galleriesService)
    {
        _galleriesService = galleriesService;
    }
    
    [HttpGet("/{GalleryId}")]
    public async Task<IActionResult> GetArtworksAsync(int GalleryId, CancellationToken token)
    {
        var response = await _galleriesService.GetArtworksAsync(GalleryId, token);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertNewExhibitionAsync([FromBody] GalleryInsertDTO purchase, CancellationToken token)
    {
        _galleriesService.InsertNewExhibitionAsync(purchase, token);
        return Ok();
    }
}