using Microsoft.EntityFrameworkCore;

public class ApplicationDb : DbContext
{
    private readonly IConfiguration? _configuration;

    public ApplicationDb(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
    {
    }
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
        if (!dbContextOptionsBuilder.IsConfigured)
        {
            dbContextOptionsBuilder.UseNpgsql(_configuration!.GetConnectionString("Database"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDb).Assembly);
    }
}


