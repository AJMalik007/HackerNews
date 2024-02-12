using HackerNews.API.Model;

namespace HackerNews.API.Integration;

public interface IHackerNewsService
{
    Task<List<long>> GetBestStoriesAsync();
    Task<StoryDetailResponse> GetStoryByIdAsync(long id);
}