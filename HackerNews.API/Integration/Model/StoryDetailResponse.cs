namespace HackerNews.API.Integration;

/// <summary>
/// Represents the response for a story detail from the HackerNews API.
/// </summary>
public class StoryDetailResponse
{
    /// <summary>
    /// Gets or sets the author of the story.
    /// </summary>
    public string By { get; set; }

    /// <summary>
    /// Gets or sets the number of descendants for the story.
    /// </summary>
    public int Descendants { get; set; }

    /// <summary>
    /// Gets or sets the ID of the story.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the list of IDs for the kids (comments) of the story.
    /// </summary>
    public List<int> Kids { get; set; }

    /// <summary>
    /// Gets the count of comments on the story.
    /// </summary>
    public int commentCounts => Kids?.Count ?? 0;

    /// <summary>
    /// Gets or sets the score of the story.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the title of the story.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the type of the story.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the URL of the story.
    /// </summary>
    public string Url { get; set; }

    private int _time;

    /// <summary>
    /// Gets the ISO formatted time of the story.
    /// </summary>
    public string IsoTime { get; private set; }

    /// <summary>
    /// Gets or sets the time of the story in Unix format. Also updates the IsoTime property.
    /// </summary>
    public int Time
    {
        get { return _time; }
        set
        {
            _time = value;
            IsoTime = DateTimeOffset.FromUnixTimeSeconds(_time).ToString("yyyy-MM-ddTHH:mm:ssK");
        }
    }
}
