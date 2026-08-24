public class Tag : Entity
{
    public string? Name { get; set; }
    
    public List<RoomTag>? RoomTags{ get; set; } 
}