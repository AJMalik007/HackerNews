namespace HackerNews.API.Model;

/// <summary>
/// Represents a response from the Top Stories API.
/// </summary>
public class TopStoriesResponse
{
    /// <summary>
    /// Gets or sets the title of the story.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the URI of the story.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Gets or sets the username of the person who posted the story.
    /// </summary>
    public string? PostedBy { get; set; }

    /// <summary>
    /// Gets or sets the time the story was posted.
    /// </summary>
    public string? Time { get; set; }

    /// <summary>
    /// Gets or sets the score of the story.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the number of comments on the story.
    /// </summary>
    public int CommentCount { get; set; }
}
