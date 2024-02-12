using HackerNews.API.Integration;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
public class StoryController : ControllerBase
{
    private readonly ILogger<StoryController> _logger;
    private readonly IHackerNewsService _hackerNewsService;

    public StoryController(
        ILogger<StoryController> logger,
        IHackerNewsService hackerNewsService)
    {
        _logger = logger;
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet]
    [Route("top")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> TopStories(int top, int pageSize, int pageNumber)
    {
        var topStories = await _hackerNewsService.GetBestStoriesAsync();


        topStories = topStories.Take(top).ToList();


        var tasks = topStories.Select(item => _hackerNewsService.GetStoryByIdAsync(item));
        var storyDetails = await Task.WhenAll(tasks);

        return Ok(storyDetails);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStory(long Id)
    {
        var topStories = await _hackerNewsService.GetStoryByIdAsync(Id);
        return Ok(topStories);
    }

}
