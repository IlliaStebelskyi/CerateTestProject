using Microsoft.EntityFrameworkCore;
using Xunit;

public class FakeCacheService : ICacheService
{
    public Task<T?> GetAsync<T>(string key, CancellationToken token = default) where T : class
        => Task.FromResult<T?>(null);

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken token = default) where T : class
        => Task.CompletedTask;

    public Task RemoveAsync(string key, CancellationToken token = default)
        => Task.CompletedTask;

    public async Task<T> GetOrSetAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken token = default) where T : class
    {
        return await factory(token);
    }
}

public class BookingTests
{
    private ApplicationDb GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDb>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDb(options);
    }

    [Fact]
    public async Task HasConflict_ReturnsTrue_WhenBookingsOverlap()
    {
        var db = GetDbContext();
        var roomId = Guid.NewGuid();

        db.Bookings.Add(new Booking
        {
            RoomId = roomId,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(1)
        });
        await db.SaveChangesAsync();

        var repo = new BookingRepository(db, new FakeCacheService());

        var hasConflict = await repo.HasConflict(
            roomId,
            DateTime.UtcNow.AddMinutes(30),
            DateTime.UtcNow.AddMinutes(90));

        Assert.True(hasConflict);
    }

    [Fact]
    public async Task HasConflict_ReturnsFalse_WhenNoOverlap()
    {
        var db = GetDbContext();
        var roomId = Guid.NewGuid();

        db.Bookings.Add(new Booking
        {
            RoomId = roomId,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(1)
        });
        await db.SaveChangesAsync();

        var repo = new BookingRepository(db, new FakeCacheService());

        var hasConflict = await repo.HasConflict(
            roomId,
            DateTime.UtcNow.AddHours(2),
            DateTime.UtcNow.AddHours(3));

        Assert.False(hasConflict);
    }
}