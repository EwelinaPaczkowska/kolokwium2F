using kolokwium2F.DTOs;

namespace kolokwium2F.Services;

public interface IGalleriesService
{
    public Task<GalleryDTO> GetArtworksAsync(int galleryId, CancellationToken token);
    public Task InsertNewExhibitionAsync(GalleryInsertDTO galleryInsertDto, CancellationToken token);
}