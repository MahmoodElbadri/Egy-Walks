using Egy_Walks.Api.Models.Domain;

namespace Egy_Walks.Api.IRepositories;

public interface IWalkRepository
{
    Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, string? sortBy, bool? sortOrder,int pageNumber, int pageSize);
    Task<Walk> CreateWalkAsync(Walk walk);
    Task<Walk?> GetWalkAsync(Guid id);
    Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
    Task DeleteWalkAsync(Guid id);
}