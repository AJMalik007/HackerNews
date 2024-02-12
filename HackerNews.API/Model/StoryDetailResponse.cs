namespace HackerNews.API.Model;

public class StoryDetailResponse
{
    public string By { get; set; }// postedBy
    public int Descendants { get; set; }
    public int Id { get; set; }
    public List<int> Kids { get; set; }//commentCount
    public int Score { get; set; }//
    public int Time { get; set; }// TODO : times needs to be in iso format
    public string Title { get; set; }//
    public string Type { get; set; }
    public string Url { get; set; }//
}
