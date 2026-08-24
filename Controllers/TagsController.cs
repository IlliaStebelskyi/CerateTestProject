using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/tags")]
public class TagsController(ITagRepository tagRepository) : ControllerBase
{
    private readonly ITagRepository _tagRepository = tagRepository;
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _tagRepository.GetAll();
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> PostTags([FromBody] Tag obj)
    {
        await _tagRepository.Add(obj);
        return CreatedAtAction(nameof(GetTags), new { id = obj.Id }, obj);
    }

}
