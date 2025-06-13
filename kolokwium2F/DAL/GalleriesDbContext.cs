using kolokwium2F.Models;
using Microsoft.EntityFrameworkCore;

namespace kolokwium2F.DAL;

public class GalleriesDbContext : DbContext
{
    public DbSet<Gallery> Gallery { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Artwork> Artwork { get; set; }
    public DbSet<Exhibition> Exhibition { get; set; }
    public DbSet<ExhibitionArtwork> ExhibitionArtwork { get; set; }

    protected GalleriesDbContext()
    {
    }

    public GalleriesDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gallery>()
            .HasKey(p => new { p.GalleryId });

        modelBuilder.Entity<Gallery>().HasData(
        new Gallery { GalleryId = 1, Name = "A", EstablishedDate = new DateTime(2025, 7, 10, 20, 0, 0)},
        new Gallery { GalleryId = 2, Name = "J", EstablishedDate = new DateTime(2025, 7, 10, 20, 0, 0)},
        new Gallery { GalleryId = 3, Name = "P", EstablishedDate = new DateTime(2025, 7, 10, 20, 0, 0)}
    );

    modelBuilder.Entity<Artist>().HasData(
        new Artist { ArtistId = 1, FirstName = "k", LastName = "A", BirthDate = new DateTime(2025, 7, 10, 20, 0, 0)},
        new Artist { ArtistId = 2, FirstName = "a", LastName = "B", BirthDate = new DateTime(2025, 7, 15, 19, 30, 0)},
        new Artist { ArtistId = 3, FirstName = "b", LastName = "C", BirthDate = new DateTime(2025, 8, 1, 18, 0, 0)}
    );

    modelBuilder.Entity<Artwork>().HasData(
        new Artwork { ArtworkId = 1, Title = "AB-001", YearCreated = 2010 },
        new Artwork { ArtworkId = 2, Title = "AB-002", YearCreated = 2002 },
        new Artwork { ArtworkId = 3, Title = "CD-001", YearCreated = 2015 },
        new Artwork { ArtworkId = 4, Title = "EF-001", YearCreated = 2025 }
    );

    modelBuilder.Entity<Exhibition>().HasData(
        new Exhibition { ExhibitionId = 1, GalleryId = 1, Title = "AB-001", StartDate = new DateTime(2025, 6, 1, 10, 0, 0), EndDate = new DateTime(2025, 7, 10, 20, 0, 0), NumberOfArtworks = 2 },
        new Exhibition { ExhibitionId = 2, GalleryId = 2, Title = "Ac-001", StartDate = new DateTime(2025, 6, 1, 10, 0, 0), EndDate = new DateTime(2025, 7, 10, 20, 0, 0), NumberOfArtworks = 2},
        new Exhibition { ExhibitionId = 3, GalleryId = 3, Title = "As-001", StartDate = new DateTime(2025, 6, 1, 10, 0, 0),EndDate = new DateTime(2025, 7, 10, 20, 0, 0), NumberOfArtworks = 2},
        new Exhibition { ExhibitionId = 4, GalleryId = 4, Title = "Aq-001", StartDate = new DateTime(2025, 6, 1, 10, 0, 0), EndDate = new DateTime(2025, 7, 10, 20, 0, 0), NumberOfArtworks = 2}
    );

    modelBuilder.Entity<ExhibitionArtwork>().HasData(
        new ExhibitionArtwork { ExhibitionId = 1, ArtworkId = 1, InsuranceValue = 2},
        new ExhibitionArtwork { ExhibitionId = 2, ArtworkId = 2, InsuranceValue = 2},
        new ExhibitionArtwork { ExhibitionId = 4, ArtworkId = 3, InsuranceValue = 2}
    );
    }
}