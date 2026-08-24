public class Building : Entity
{
    public string? Name { get; set; }
    public Address? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public List<Room> Rooms { get; set; } = [];
}