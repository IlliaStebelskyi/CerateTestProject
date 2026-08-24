public interface IUserRepository : IGenericRepository<User>
{
    public Task<List<User>> GetTopLevelUsers();
    public Task<List<User>> GetSubordinatesTree(Guid ManagerId);
    public Task<User?> GetWithProfile(Guid UserId);
    public Task<List<User>> GetByCompanyId(Guid CompanyId);
}