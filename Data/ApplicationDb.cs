using Microsoft.EntityFrameworkCore;
public class ApplicationDb(IConfiguration configuration) : DbContext
{
    private readonly IConfiguration _configuration = configuration;
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RoomTag> RoomTags { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        dbContextOptionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDb).Assembly);
    }

}