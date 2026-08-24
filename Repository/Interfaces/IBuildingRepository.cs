public interface IBuildingRepository : IGenericRepository<Building>
{
    public Task<List<Tag>> GetDistinctTags(Guid BuildingId, CancellationToken token = default);
    public Task<Building?> GetWithRooms(Guid BuildingId);
}