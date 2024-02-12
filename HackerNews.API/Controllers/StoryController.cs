using HackerNews.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpGet]
    [Route("top")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TopStories(int top, int pageSize = 10, int pageNumber = 1)
    {
        var storyDetails = await _storyService.GetTopStories(top, pageSize, pageNumber);
        return Ok(storyDetails);
    }
}
