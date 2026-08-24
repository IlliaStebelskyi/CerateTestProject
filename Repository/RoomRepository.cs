using Microsoft.EntityFrameworkCore;

public class RoomRepository(ApplicationDb applicationDb, ICacheService cacheService) : GenericRepository<Room>(applicationDb), IRoomRepository
{
    private readonly ApplicationDb _applicationDb = applicationDb;
    private readonly ICacheService _cacheService = cacheService;
    private static readonly TimeSpan TimerRoomCache = TimeSpan.FromSeconds(15);
    private static string GetRoomRedis(Guid BuildingId)
    {
        return $"roomId:{BuildingId}:room";
    }

    private static string GetChangeRoomRedis(Guid roomId)
    {
        return $"roomId:{roomId}:room";
    }

    public async Task<List<Room>> GetAvailableRooms(Guid BuildingId, DateTime start, DateTime end)
    {
        return await _applicationDb.Rooms
            .Where(c => c.BuildingId == BuildingId)
            .Where(r => !r.Bookings.Any(b => b.StartTime < end && start < b.EndTime))
            .ToListAsync();
    }

    public Task<List<Room>> GetByBuildingId(Guid BuildingId)
    {
        return _cacheService.GetOrSetAsync(GetRoomRedis(BuildingId),
            async ct => await _applicationDb.Rooms
                .Where(c => c.BuildingId == BuildingId)
                .ToListAsync(ct),
        TimerRoomCache
        );
    }


    public async Task<List<Room>> GetByTags(List<Guid> TagsID)
    {
        return await _applicationDb.Rooms
            .Where(r => TagsID.All(tagId => r.RoomTags.Any(rt => rt.TagId == tagId)))
            .ToListAsync();
    }

    public async Task<decimal> GetOccupancyRate(Guid roomId, DateTime start, DateTime end)
    {
        var bookings = await _applicationDb.Bookings
            .Where(b => b.RoomId == roomId && b.StartTime >= start && b.EndTime <= end)
            .ToListAsync();

        var totalBookedTime = bookings.Sum(b => (b.EndTime - b.StartTime).TotalHours);
        double totalPeriodTime = (end - start).TotalHours;

        return (decimal)(totalBookedTime / totalPeriodTime * 100);
    }

    public async Task<Room> ChangeRoomCapacity(Guid roomId, int newCapacity)
    {
        var room = await _applicationDb.Rooms.FindAsync(roomId) ?? throw new ArgumentException();
        room.Capacity = newCapacity;
        await _applicationDb.SaveChangesAsync();
        await _cacheService.RemoveAsync(GetChangeRoomRedis(roomId));
        return room;
    }


    private async Task<bool> RoomExistsInCache(Guid roomId)
    {
        var cache = await _cacheService.GetAsync<Room>(GetRoomRedis(roomId));
        return cache is not null;
    }
}
