using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

public class BookingRepository(ApplicationDb applicationDb, ICacheService cacheService) : GenericRepository<Booking>(applicationDb), IBookingRepository
{
    private readonly ApplicationDb _applicationDb = applicationDb;
    private readonly ICacheService _cacheService = cacheService;
    private static readonly TimeSpan DelayCache = TimeSpan.FromSeconds(10);
    private static string GetTopBookedRoomsCache(int count, DateTime start, DateTime end)
    {
        return $"cache:{count}, {start}, {end}";
    }
    private static string GetBookingCountsByUserCache() { return $"countbook:user"; }
    private static string CreateBookingWithRoomRefCache(Guid roomId, Guid userId, DateTime start, DateTime end) { return $"{roomId}, {userId}, {start}, {end}"; }

    public Task<Dictionary<Guid, int>> GetBookingCountsByUser()
    {
        return _cacheService.GetOrSetAsync(GetBookingCountsByUserCache(),
        async ct => await _applicationDb.Bookings
            .GroupBy(c => c.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                Count = g.Count()
            })
            .ToDictionaryAsync(x => x.UserId, x => x.Count, ct), DelayCache
            );
    }



    public async Task<List<Booking>> GetByRoomData(Guid RoomId, DateTime dateTime)
    {
        return await _applicationDb.Bookings.Where(c => c.RoomId == RoomId).Where(c => c.StartTime == dateTime).ToListAsync();
    }

    public async Task<List<Booking>> GetByUserId(Guid UserId)
    {
        return await _applicationDb.Bookings
        .Where(c => c.UserId == UserId)
        .OrderBy(c => c.StartTime)
        .ToListAsync();
    }

    public async Task<List<User>> GetReliableUsers()
    {
        return await _applicationDb.Users
        .Where(u => u.Bookings.Any(b => b.Status == BookingStatus.Confirmed))
        .Where(u => !u.Bookings.Any(b => b.Status == BookingStatus.Cancelled))
        .ToListAsync();
    }

    public async Task<List<Room>> GetTopBookedRooms(int count, DateTime start, DateTime end)
    {

        List<Guid> cache = await _cacheService.GetOrSetAsync(GetTopBookedRoomsCache(count, start, end),
           async ct => await _applicationDb.Bookings
           .Where(c => c.StartTime >= start && c.EndTime <= end)
           .GroupBy(c => c.RoomId)
           .OrderByDescending(c => c.Count())
           .Take(count)
           .Select(c => c.Key)
           .ToListAsync(ct), DelayCache
       );

        return await _applicationDb.Rooms
            .Where(c => cache.Contains(c.Id))
            .ToListAsync();
    }

    public async Task<bool> HasConflict(Guid roomId, DateTime start, DateTime end, Guid? excludeBookingId = null)
    {
        return await _applicationDb.Bookings
            // берём только брони этой конкретной комнаты
            .Where(b => b.RoomId == roomId)
            // если передан excludeBookingId (например, редактируем существующую бронь) —
            // исключаем её саму из проверки, иначе она "конфликтовала" бы сама с собой
            .Where(b => excludeBookingId == null || b.Id != excludeBookingId)
            // проверяем, есть ли среди оставшихся броней хотя бы одна,
            // которая пересекается по времени с новым интервалом [start, end]
            .AnyAsync(b => b.StartTime < end && start < b.EndTime);
    }


    public async Task<Booking> CreateBookingWithRoomRef(Guid roomId, Guid userId, DateTime start, DateTime end)
    {
        var room = new Room { Id = roomId };
        _applicationDb.Attach(room);

        var newBooking = new Booking
        {
            RoomId = roomId,
            UserId = userId,
            StartTime = start,
            EndTime = end
        };


        _applicationDb.Bookings.Add(newBooking);

        await _applicationDb.SaveChangesAsync();

        await _cacheService.RemoveAsync(GetBookingCountsByUserCache());

        return newBooking;
        // используй Attach для Room, чтобы избежать лишнего запроса
    }

}