using Egy_Walks.Api.Data;
using Egy_Walks.Api.IRepositories;
using Egy_Walks.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Egy_Walks.Api.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<WalkRepository> _logger;

    public WalkRepository(ApplicationDbContext db, ILogger<WalkRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null)
    {
        _logger.LogInformation("Getting all walks");
        var walks = _db.Walks
            .Include(tmp => tmp.Difficulty)
            .Include(tmp => tmp.Region)
            .AsQueryable();

        //filtering
        if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.CurrentCultureIgnoreCase))
            {
                walks = walks.Where(tmp => tmp.Name.Contains(filterQuery));
            }

            if (filterOn.Equals("Description", StringComparison.CurrentCultureIgnoreCase))
            {
                walks = walks.Where(tmp => tmp.Description.Contains(filterQuery));
            }
        }

        // return await _db.Walks
        //     .Include(tmp => tmp.Difficulty)
        //     .Include(tmp => tmp.Region)
        //     .ToListAsync();
        return await walks.ToListAsync();
    }

    public async Task<Walk> CreateWalkAsync(Walk walk)
    {
        _logger.LogInformation("Creating walk with ID {WalkId}.", walk.Id);
        await _db.Walks.AddAsync(walk);
        await _db.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> GetWalkAsync(Guid id)
    {
        _logger.LogInformation("Getting walk with ID {WalkId}.", id);
        var walk = await _db.Walks
            .Include(tmp => tmp.Difficulty)
            .Include(tmp => tmp.Region)
            .FirstOrDefaultAsync(tmp => tmp.Id == id);
        return walk;
    }

    public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
    {
        _logger.LogInformation("Updating walk with ID {WalkId}.", id);

        var existingWalk = await _db.Walks.FindAsync(id);
        if (existingWalk == null)
        {
            throw new KeyNotFoundException($"Walk with id {id} not found.");
        }

        existingWalk.Name = walk.Name;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;
        await _db.SaveChangesAsync();
        return existingWalk;
    }

    public async Task DeleteWalkAsync(Guid id)
    {
        _logger.LogInformation($"Deleting walk with ID {id}.");
        var walk = await _db.Walks.FindAsync(id);
        if (walk == null)
        {
            throw new KeyNotFoundException($"Walk with id {id} not found.");
        }

        _logger.LogInformation("Deleting walk with ID {WalkId}.", id);
        _db.Walks.Remove(walk);
        await _db.SaveChangesAsync();
    }
}