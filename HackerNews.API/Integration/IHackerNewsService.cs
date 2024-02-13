using HackerNews.API.Model;

namespace HackerNews.API.Integration
{
    /// <summary>
    /// Defines the contract for a service that interacts with the Hacker News API.
    /// </summary>
    public interface IHackerNewsService
    {
        /// <summary>
        /// Asynchronously retrieves the IDs of the best stories from Hacker News.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of story IDs.</returns>
        Task<List<long>> GetBestStoriesAsync();

        /// <summary>
        /// Asynchronously retrieves a story from Hacker News by its ID.
        /// </summary>
        /// <param name="id">The ID of the story.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the story data.</returns>
        Task<TopStoriesResponse> GetStoryByIdAsync(long id);
    }
}