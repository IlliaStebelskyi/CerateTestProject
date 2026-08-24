using Microsoft.EntityFrameworkCore;

public class TagRepository(ApplicationDb applicationDb) : GenericRepository<Tag>(applicationDb), ITagRepository
{
    private readonly ApplicationDb applicationDb1 = applicationDb;

    public async Task<Tag> CreateTag(string name)
    {
        var tag = new Tag { Name = name }; // создаём сущность
        applicationDb1.Add(tag); // Added
        await applicationDb1.SaveChangesAsync(); // INSERT
        return tag; // после SaveChanges у tag уже будет реальный Id из базы
    }

    public async Task<Room> UpdateRoom(Room updatedRoom)
    {
        applicationDb1.Update(updatedRoom);
        await applicationDb1.SaveChangesAsync();
        return updatedRoom;
    }

    public async Task<User> UpdateEmail(Guid userId, string newEmail)
    {
        var user = await applicationDb1.Users.FindAsync(userId);
        applicationDb1.Attach(user);
        user?.Email = newEmail;
        await applicationDb1.SaveChangesAsync();
        return user;
    }

}