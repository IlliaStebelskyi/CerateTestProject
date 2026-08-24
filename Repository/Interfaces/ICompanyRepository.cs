public interface ICompanyRepository : IGenericRepository<Company>
{
    public Task<Company?> GetWithUsers(Guid CompanyId);
}