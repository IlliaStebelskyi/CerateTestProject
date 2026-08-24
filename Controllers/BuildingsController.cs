using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/buildings")]
public class BuildingController(IBuildingRepository buildingRepository, IRoomRepository roomRepository) : ControllerBase
{
    private readonly IBuildingRepository _buildingRepository = buildingRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;

    [HttpGet]
    public async Task<IActionResult> GetBuildings()
    {
        var result = await _buildingRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBuildingById(Guid id)
    {
        var building = await _buildingRepository.GetWithRooms(id);
        if (building == null)
            return NotFound();
        return Ok(building);
    }

    [HttpGet("{id}/tags")]
    public async Task<IActionResult> GetDistinctTags(Guid id)
    {
        var tags = await _buildingRepository.GetDistinctTags(id);
        return Ok(tags);
    }

    [HttpGet("{id}/rooms")]
    public async Task<IActionResult> GetRoomsByTags(Guid id, [FromQuery] List<Guid> tags)
    {
        var rooms = await _roomRepository.GetByTags(tags);
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<IActionResult> PostBuilding([FromBody] Building building)
    {
        await _buildingRepository.Add(building);
        return CreatedAtAction(nameof(GetBuildingById), new { id = building.Id }, building);
    }
}