using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class Linq(ApplicationDb applicationDb)
{
    private readonly ApplicationDb _applicationDb = applicationDb;

    public async Task<List<Booking>> Booking(Guid id)
    {
        DateTime last7days = DateTime.UtcNow.AddDays(-7);
        return await _applicationDb.Bookings.AsNoTracking()
        .Where(b => b.UserId == id && b.CreatedAt >= last7days).OrderByDescending(c => c.CreatedAt).ToListAsync();
    }


    public record BuildingRoomCountDto(string BuildingName, int RoomCount);

    public async Task<List<BuildingRoomCountDto>> GetRoomCountPerBuildingAsync()
    {
        return await _applicationDb.Rooms
            .AsNoTracking()
            .GroupBy(r => new { r.BuildingId, r.Building.Name })
            .Select(g => new BuildingRoomCountDto(
                g.Key.Name,
                g.Count()
            ))
            .ToListAsync();
    }


    public async Task<List<object>> GetUsersWithConfirmedBookingsAsync()
    {
        // return await _applicationDb.Users
        //     .AsNoTracking()
        //     .Where(u => u.Bookings.Any(b => b.Status == BookingStatus.Confirmed))
        //     .ToListAsync();

        var query = from w in _applicationDb.Users
                    let confirmed = w.Bookings.Count(c => c.Status == BookingStatus.Confirmed)
                    where confirmed > 0
                    select new { User = w, Conf = confirmed };
        return await query.AsNoTracking().ToListAsync<object>();
    }


    public async Task<List<Tag?>> GetTags(Guid id)
    {
        return await _applicationDb.Rooms
        .Where(c => c.BuildingId == id)
        .SelectMany(r => r.RoomTags.Select(rt => rt.Tag))
        .ToListAsync();
    }


    public async Task<List<Guid>> GetTopActiveUserIds()
    {
        return await _applicationDb.Bookings
            .GroupBy(b => b.UserId)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key)
            .ToListAsync();
    }


    public async Task<List<Room>> Gfdas()
    {
        var roomsWithProj = _applicationDb.RoomTags.Where(r => r.Tag.Name == "Projector").Select(c => c.RoomId);
        var roomsWithQuiet = _applicationDb.RoomTags.Where(c => c.Tag.Name == "Quiet").Select(c => c.RoomId);

        var result = await roomsWithProj.Except(roomsWithQuiet).ToListAsync();

        return await _applicationDb.Rooms
        .Where(r => result.Contains(r.Id))
        .ToListAsync();
    }


    public async Task<TimeSpan> GetTotalDuration(Guid roomId)
    {
        var bookings = await _applicationDb.Bookings
            .Where(b => b.RoomId == roomId)
            .ToListAsync();

        return bookings.Aggregate(TimeSpan.Zero, (total, b) => total + (b.EndTime - b.StartTime));
    }


    public async Task<List<object>> Counter()
    {
        return await _applicationDb.Bookings.GroupBy(c => new { c.RoomId, c.Status }).Select(c => new
        {
            RoomId = c.Key.RoomId,
            Status = c.Key.Status,
            Count = c.Count()
        }).ToListAsync<object>();
    }



}