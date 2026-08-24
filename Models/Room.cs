public class Room : Entity
{
    public Guid BuildingId { get; set; }
    public string? Number { get; set; }
    public int Capacity { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<RoomTag> RoomTags { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    public Building? Building { get; set; }
}