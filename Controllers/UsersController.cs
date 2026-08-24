using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpGet("{id}/subordinates")]
    public async Task<IActionResult> TreeSubordinates(Guid id)
    {
        var result = await _userRepository.GetSubordinatesTree(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User user)
    {
        await _userRepository.Add(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }
}