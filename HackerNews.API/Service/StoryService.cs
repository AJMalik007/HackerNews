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
        _hackerNewsService = hackerNewsService ?? throw new ArgumentNullException(nameof(hackerNewsService));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<TopStoriesResponse>> GetTopStories(int top, int pageSize, int pageNumber)
    {
        const string cacheKey = "TopStories";
        List<long> topStories;

        // Try to get the top stories from the cache
        if (!_memoryCache.TryGetValue(cacheKey, out topStories))
        {
            _logger.LogInformation("Top stories not found in cache. Fetching from service.");

            // If the top stories are not in the cache, get them from the service
            topStories = await _hackerNewsService.GetBestStoriesAsync();

            // Set the cache options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Cache for 10 mins

            // Save the top stories in the cache
            _memoryCache.Set(cacheKey, topStories, cacheEntryOptions);

            _logger.LogInformation("Top stories fetched from service and stored in cache.");
        }
        else
        {
            _logger.LogInformation("Top stories found in cache.");
        }

        // Take the top stories first
        topStories = topStories.Take(top).ToList();

        // Then apply pagination on the top stories
        var paginatedStories = topStories.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        // Fetch story details for each story in parallel
        var tasks = paginatedStories.Select(_hackerNewsService.GetStoryByIdAsync);
        var storyDetails = await Task.WhenAll(tasks);

        _logger.LogInformation($"Fetched details for {storyDetails.Length} stories.");

        return storyDetails.ToList();
    }
}
