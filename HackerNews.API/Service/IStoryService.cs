using HackerNews.API.Model;

namespace HackerNews.API.Service;

public interface IStoryService
{
    Task<List<TopStoriesResponse>> GetTopStories(int top, int pageSize, int pageNumber);
}
