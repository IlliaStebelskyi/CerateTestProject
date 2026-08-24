public class Booking : Entity
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime EndTime { get; set; } = DateTime.UtcNow;
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public Room? Room { get; set; }
}