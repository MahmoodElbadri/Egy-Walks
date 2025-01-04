using Egy_Walks.Api.Data;
using Egy_Walks.Api.IRepositories;
using Egy_Walks.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Egy_Walks.Api.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<RegionRepository> _logger;

    public RegionRepository(ApplicationDbContext db, ILogger<RegionRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Region>> GetAllRegionsAsync()
    {
        _logger.LogInformation("Getting all regions");
        return await _db.Regions.ToListAsync();
    }

    public async Task<Region?> GetRegionAsync(Guid id)
    {
        var region = await _db.Regions.Where(tmp => tmp.Id == id).FirstOrDefaultAsync();
        if (region == null)
        {
            return null;
        }

        return region;
    }

    public async Task<Region> CreateRegionAsync(Region region)
    {
        await _db.Regions.AddAsync(region);
        await _db.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
    {
        var existingRegion = await _db.Regions.FirstOrDefaultAsync(tmp => tmp.Id == id);
        if (existingRegion == null)
        {
            return null;
        }

        existingRegion.RegionImageUrl = region.RegionImageUrl;
        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        await _db.SaveChangesAsync();
        return existingRegion;
    }

    public async Task DeleteRegionAsync(Guid id)
    {
        var region = await _db.Regions.FindAsync(id);
        if (region == null)
        {
            throw new KeyNotFoundException($"Region with id {id} not found.");
        }
        _db.Regions.Remove(region);
        await _db.SaveChangesAsync();
    }
}