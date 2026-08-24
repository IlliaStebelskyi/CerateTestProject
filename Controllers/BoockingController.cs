using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/bookings")]
public class BookingController(IBookingRepository bookingRepository) : ControllerBase
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    [HttpGet("~/api/users/{userId}/bookings")]
    [TypeFilter<LoggingFilter>(Arguments = ["booking"])]
    public async Task<IActionResult> GetBookings(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserId(userId);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> PostBookings([FromBody] Booking booking)
    {
        var hasConflict = await _bookingRepository.HasConflict(booking.RoomId, booking.StartTime, booking.EndTime);

        if (hasConflict)
        {
            return Conflict(new { error = "Room is already booked for this time" });
        }

        await _bookingRepository.Add(booking);

        return CreatedAtAction(nameof(GetBookings), new { userId = booking.UserId }, booking);
    }

    [HttpPatch("/{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var booking = await _bookingRepository.GetById(id);
        if (booking == null)
            return NotFound();

        booking.Status = BookingStatus.Cancelled;
        await _bookingRepository.Update(booking);
        return Ok(booking);
    }

}