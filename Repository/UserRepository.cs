using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class UserRepository(ApplicationDb applicationDb) : GenericRepository<User>(applicationDb), IUserRepository
{
    private readonly ApplicationDb _applicationDb = applicationDb;

    public async Task<User> CreateUser(string email, string firstName, string lastName, Guid companyId)
    {
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            CompanyId = companyId
        };

        _applicationDb.Users.Add(user);
        await _applicationDb.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetByCompanyId(Guid CompanyId)
    {
        return await _applicationDb.Users.Where(c => c.CompanyId == CompanyId).ToListAsync();
    }

    public async Task<List<User>> GetSubordinatesTree(Guid ManagerId)
    {
        var allUsers = await _applicationDb.Users.ToListAsync();

        var result = new List<User>();
        CollectSubordinates(ManagerId, allUsers, result);
        return result;
    }

    private void CollectSubordinates(Guid managerId, List<User> allUsers, List<User> result)
    {
        var direct = allUsers.Where(u => u.ManagerId == managerId).ToList();
        result.AddRange(direct);

        foreach (var subordinates in direct)
        {
            CollectSubordinates(subordinates.Id, allUsers, result);
        }
    }

    public async Task<List<User>> GetTopLevelUsers()
    {
        return await _applicationDb.Users.Where(c => c.ManagerId == null).ToListAsync();
    }

    public async Task<User?> GetWithProfile(Guid UserId)
    {
        return await _applicationDb.Users.Include(c => c.UserProfile).FirstOrDefaultAsync(c => c.Id == UserId);
    }

}