using Microsoft.EntityFrameworkCore;

public class CompanyRepository(ApplicationDb applicationDb) : GenericRepository<Company>(applicationDb), ICompanyRepository
{
    private readonly ApplicationDb _applicationDb = applicationDb;

    public async Task<Company?> GetWithUsers(Guid CompanyId)
    {
        return await _applicationDb.Companies.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == CompanyId);
    }

    

}