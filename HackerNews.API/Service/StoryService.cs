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

    public async Task<List<TopStoriesResponse>> GetTopStories(int top, string titleFilter = null)
    {
        const string cacheKey = "TopStories";
        const string cacheKeyAllStories = "AllStories";
        List<long> topStories;
        List<TopStoriesResponse> allStories;

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

        // Try to get all stories from the cache
        if (!_memoryCache.TryGetValue(cacheKeyAllStories, out allStories))
        {
            // Fetch story details for each story in parallel
            var tasks = topStories.Select(_hackerNewsService.GetStoryByIdAsync);
            allStories = (await Task.WhenAll(tasks)).ToList();

            // Set the cache options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // Cache for 10 mins

            // Save all stories in the cache
            _memoryCache.Set(cacheKeyAllStories, allStories, cacheEntryOptions);
        }

        // Filter stories by title if filter is provided
        if (!string.IsNullOrEmpty(titleFilter))
        {
            allStories = allStories.Where(s => s.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Take the top stories first
        topStories = topStories.Take(top).ToList();

        _logger.LogInformation($"Fetched details for {allStories.Count} stories.");

        return allStories;
    }
}
