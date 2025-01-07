using Egy_Walks.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Egy_Walks.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Region> Regions { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Walk> Walks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        List<Region> regions = new() {
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Egyptian Musuem",
                Code = "Egy Mus",
                RegionImageUrl = "egy-mus.jpg"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Pyramids",
                Code = "Pyramids",
                RegionImageUrl = "pyramids.jpg"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "St Catherine's Mountain",
                Code = "St Cath Mtn",
                RegionImageUrl = "mountain.jpg"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Cairo",
                Code = "Cairo",
                RegionImageUrl = "cairo.jpg"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "Helwan",
                Code = "Helwan",
                RegionImageUrl = "helwan.jpg"
            },
            new Region
            {
                Id = Guid.NewGuid(),
                Name = "The Nile",
                Code = "The Nile",
                RegionImageUrl = "nile.jpg"
            }
        };

        modelBuilder.Entity<Region>().HasData(regions);
        
        List<Difficulty> difficulties = new List<Difficulty>() {
            new Difficulty()
            {
                Id = Guid.NewGuid(),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.NewGuid(),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.NewGuid(),
                Name = "Hard"
            }
        };
        modelBuilder.Entity<Difficulty>().HasData(difficulties);
        
        
    }
}