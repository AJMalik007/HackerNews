using HackerNews.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;
    private readonly ILogger<StoryController> _logger;

    public StoryController(IStoryService storyService, ILogger<StoryController> logger)
    {
        _storyService = storyService ?? throw new ArgumentNullException(nameof(storyService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("top")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTopStories(int top, string? titleFilter)
    {

        try
        {
            var storyDetails = await _storyService.GetTopStories(top, titleFilter);
            return Ok(storyDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting top stories.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
