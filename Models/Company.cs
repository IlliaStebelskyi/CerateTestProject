public class Company : Entity
{
    public required string Name { get; set; }
    public string? RegisterNum { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    public List<User> Users { get; set; } = [];
}