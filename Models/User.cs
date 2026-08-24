public class User : Entity
{
    public Guid CompanyId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid? ManagerId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public UserProfile? UserProfile { get; set; }
    public Company? Company { get; set; }
    public User? Manager { get; set; }
    public List<User> Subordinates { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
}