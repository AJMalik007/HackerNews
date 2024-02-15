using HackerNews.API.Model;

namespace HackerNews.API.Service;

/// <summary>
/// Provides functionality to interact with HackerNews stories.
/// </summary>
public interface IStoryService
{
    /// <summary>
    /// Retrieves a list of top stories from HackerNews.
    /// </summary>
    /// <param name="top">The number of top stories to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <param name="pageNumber">The number of the page to retrieve.</param>
    /// <returns>A list of top stories.</returns>
    Task<List<TopStoriesResponse>> GetTopStories(int top, string titleFilter = null);
}
