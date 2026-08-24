public interface IBookingRepository : IGenericRepository<Booking>
{
    public Task<List<Booking>> GetByUserId(Guid UserId);
    public Task<List<Booking>> GetByRoomData(Guid RoomId, DateTime dateTime);
    public Task<bool> HasConflict(Guid RoomId, DateTime start, DateTime end, Guid? excludeBookingId = null);
    Task<List<Room>> GetTopBookedRooms(int count, DateTime start, DateTime end);
    Task<Dictionary<Guid, int>> GetBookingCountsByUser();
    public Task<List<User>> GetReliableUsers();
}