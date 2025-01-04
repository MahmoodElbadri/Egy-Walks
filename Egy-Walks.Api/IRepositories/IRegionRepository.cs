using Egy_Walks.Api.Models.Domain;

namespace Egy_Walks.Api.IRepositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllRegionsAsync();
    Task<Region?> GetRegionAsync(Guid id);
    Task<Region> CreateRegionAsync(Region region);
    Task<Region?> UpdateRegionAsync(Guid id, Region region);
    Task DeleteRegionAsync(Guid id);
}