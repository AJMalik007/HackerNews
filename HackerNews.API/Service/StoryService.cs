using HackerNews.API.Integration;
using HackerNews.API.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.API.Service;
public class StoryService : IStoryService
{
    private readonly IHackerNewsService _hackerNewsService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<StoryService> _logger;

    public StoryService(
        IHackerNewsService hackerNewsService,
        IMemoryCache memoryCache,
        ILogger<StoryService> logger)
    {
        _hackerNewsService = hackerNewsService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<List<TopStoriesResponse>> GetTopStories(int top, int pageSize, int pageNumber)
    {
        var cacheKey = "TopStories";
        List<long> topStories;

        // Try to get the top stories from the cache
        if (!_memoryCache.TryGetValue(cacheKey, out topStories))
        {
            // If the top stories are not in the cache, get them from the service
            topStories = await _hackerNewsService.GetBestStoriesAsync();

            // Set the cache options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Cache for 10 mins

            // Save the top stories in the cache
            _memoryCache.Set(cacheKey, topStories, cacheEntryOptions);
        }

        // Take the top stories first
        topStories = topStories.Take(top).ToList();

        // Then apply pagination on the top stories
        var paginatedStories = topStories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        var tasks = paginatedStories.Select(item => _hackerNewsService.GetStoryByIdAsync(item));
        var storyDetails = await Task.WhenAll(tasks);

        return storyDetails.ToList();
    }
}
