namespace HackerNews.API.Integration
{
    /// <summary>
    /// Represents the response for the best stories from the HackerNews API.
    /// </summary>
    public class BestStoriesResponse
    {
        /// <summary>
        /// Gets or sets the list of IDs for the best stories.
        /// </summary>
        public List<int> Items { get; set; }
    }
}
