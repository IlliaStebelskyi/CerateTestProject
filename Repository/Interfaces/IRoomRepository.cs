public interface IRoomRepository : IGenericRepository<Room>
{
    public Task<List<Room>> GetAvailableRooms(Guid BuildingId, DateTime start, DateTime end);
    public Task<List<Room>> GetByTags(List<Guid> TagsId);
    public Task<decimal> GetOccupancyRate(Guid RoomId, DateTime start, DateTime end);
    public Task<List<Room>> GetByBuildingId(Guid BuildingId);
}