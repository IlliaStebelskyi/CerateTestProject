using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

public class BuildingRepository(ApplicationDb applicationDb, ICacheService cacheService) : GenericRepository<Building>(applicationDb), IBuildingRepository
{
    private readonly ApplicationDb _applicationDb = applicationDb;
    private readonly ICacheService _cacheService = cacheService;

    private static readonly TimeSpan TagsCacheDuration = TimeSpan.FromSeconds(30);
    private static string BuildingTagsCacheKey(Guid buildingId) => $"building:{buildingId}:tags";

    public Task<List<Tag?>> GetDistinctTags(Guid buildingId, CancellationToken token = default)
    {
        return _cacheService.GetOrSetAsync(
            BuildingTagsCacheKey(buildingId),
            async ct => await _applicationDb.RoomTags
                .Where(rt => rt.Room.BuildingId == buildingId)
                .Select(rt => rt.Tag)
                .Distinct()
                .ToListAsync(ct),
            TagsCacheDuration,
            token);
    }


    public async Task<Building?> GetWithRooms(Guid BuildingId)
    {
        return await _applicationDb.Buildings.Include(c => c.Rooms).FirstOrDefaultAsync(c => c.Id == BuildingId);
    }
}