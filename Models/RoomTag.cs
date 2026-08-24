public class RoomTag
{
    public Guid RoomId { get; set; }
    public Guid TagId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public Room? Room { get; set; }
    public Tag? Tag { get; set; }
}