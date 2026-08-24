using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rooms")]
public class RoomsController(IRoomRepository roomRepository, IBookingRepository bookingRepository) : ControllerBase
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var result = await _roomRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{roomId}/availability")]
    public async Task<IActionResult> GetAvailability(Guid roomId, [FromQuery] DateTime date)
    {
        var bookings = await _bookingRepository.GetByRoomData(roomId, date);
        return Ok(bookings);
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRooms([FromQuery] int count, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var result = await _bookingRepository.GetTopBookedRooms(count, start, end);
        return Ok(result);
    }
}