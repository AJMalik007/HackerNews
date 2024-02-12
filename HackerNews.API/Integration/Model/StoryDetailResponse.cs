namespace HackerNews.API.Integration;

public class StoryDetailResponse
{
    public string By { get; set; }
    public int Descendants { get; set; }
    public int Id { get; set; }
    public int Score { get; set; }
    private int _time;
    public string Title { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
    public int commentCounts => Kids?.Count ?? 0;

    public int Time
    {
        get { return _time; }
        set
        {
            _time = value;
            IsoTime = DateTimeOffset.FromUnixTimeSeconds(_time).ToString("o");
        }
    }
    public string IsoTime { get; private set; }
    public List<int> Kids { get; set; }
}
