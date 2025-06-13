using kolokwium2F.DAL;
using kolokwium2F.DTOs;
using kolokwium2F.Exceptions;
using kolokwium2F.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2F.Services;

public class GalleriesService : IGalleriesService
{
    private readonly GalleriesDbContext _dbContext;

    public GalleriesService(GalleriesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GalleryDTO> GetArtworksAsync(int GalleryId, CancellationToken token)
    {
        var gallery = await _dbContext.Gallery.FindAsync(GalleryId, token);
        if (gallery == null)
            throw new NotFoundException("nie znaleziono galerii");

        var galleryDto = new GalleryDTO()
        {
            Name = gallery.Name,
            EstablishedDate = gallery.EstablishedDate
        };
        List<ExhibitionDTO> exhibitionDtos = new List<ExhibitionDTO>();

        var pull = await _dbContext.Exhibition.Where(p => p.ExhibitionId == GalleryId).ToListAsync(token);
        
        foreach (var pdto in pull)
        {
            var exhibitionDto = new ExhibitionDTO()
            {
                Title = pdto.Title,
                StartDate = pdto.StartDate,
                EndDate = pdto.EndDate,
                NumberOfArtworks = pdto.NumberOfArtworks
            };
            
            
            

            /*var concertDto = await _dbContext.Artwork.Where(p => p.ExhibitionArtwork == pdto.ExhibitionId)
                .Select(p => new ArtworkDTO()
                {
                    Title = p.Gallery.Title,
                    Date = p.Gallery.Date
                }).FirstOrDefaultAsync(token);
            exhibitionDto.concert = concertDto;
            exhibitionDtos.Add(exhibitionDto);*/
        }

        //galleryDto.Purchases = exhibitionDtos;
        return galleryDto;
    }

    public async Task InsertNewExhibitionAsync(GalleryInsertDTO galleryInsertDto, CancellationToken token)
{
    await using var transaction = await _dbContext.Database.BeginTransactionAsync(token);

    try
    {
        var customer = await _dbContext.Gallery.FindAsync(new object[] { galleryInsertDto.Gallery.GalleryId }, token);
        if (customer == null)
        {
            customer = new Gallery
            {
                GalleryId = galleryInsertDto.Gallery.GalleryId,
                Name = galleryInsertDto.Gallery.Name,
            };
            await _dbContext.Gallery.AddAsync(customer, token);
        }

        var groupedByConcert = galleryInsertDto.Artworks
            .GroupBy(p => p.Title)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var exhibitions in galleryInsertDto.Artworks)
        {
            var gallery = await _dbContext.Gallery.FirstOrDefaultAsync(c => c.Name == exhibitions.Title, token);
            if (gallery == null)
                throw new NotFoundException($"Wystawy '{exhibitions.Title}' nie znaleziono");

            var artw = new Artwork
            {
                ArtworkId = await _dbContext.Artwork.MaxAsync(t => (int?)t.ArtworkId, token) ?? 1,
                InsuranceValue = exhibitions.InsuranceValue
            };

            await _dbContext.Artwork.AddAsync(artw, token);
        }

        await _dbContext.SaveChangesAsync(token);

        await transaction.CommitAsync(token);
    }
    catch
    {
        await transaction.RollbackAsync(token);
        throw;
    }
}
}