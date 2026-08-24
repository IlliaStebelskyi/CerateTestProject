public class UserProfile : Entity
{
    public Guid UserId { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; } 

    public User? User { get; set; }
}