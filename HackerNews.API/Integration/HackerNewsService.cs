using HackerNews.API.Model;
using Newtonsoft.Json;

namespace HackerNews.API.Integration;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HackerNewsService> _logger;

    public HackerNewsService(
        IHttpClientFactory httpClientFactory,
        ILogger<HackerNewsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<long>> GetBestStoriesAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("HackerNewsApi");
            _logger.LogInformation("Client Created Successfully");

            var uri = $"/v0/beststories.json";

            var result = await httpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Get request to ELMS successful");

                string resultContent = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"Get the response from ELMS {resultContent}");

                var propertyParser = JsonConvert.DeserializeObject<List<long>>(resultContent);
                return propertyParser;
            }
            else
            {
                _logger.LogError("Get request to HackerNewsApi failed");
                throw new Exception("Get request to HackerNewsApi failed");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<TopStoriesResponse> GetStoryByIdAsync(long id)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("HackerNewsApi");
            _logger.LogInformation("Client Created Successfully");

            var uri = $"/v0/item/{id}.json";

            var result = await httpClient.GetAsync(uri);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation("Get request to HackerNewsApi successful");

                string resultContent = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"Get the response from HackerNewsApi {resultContent}");

                var propertyParser = JsonConvert.DeserializeObject<StoryDetailResponse>(resultContent);

                if (propertyParser != null)
                {
                    var story = new TopStoriesResponse
                    {
                        Title = propertyParser.Title,
                        Uri = propertyParser.Url,
                        PostedBy = propertyParser.By,
                        Time = propertyParser.IsoTime,
                        Score = propertyParser.Score,
                        CommentCount = propertyParser.commentCounts
                    };
                    return story;
                }
                return null;
            }
            else
            {
                _logger.LogError("Get request to HackerNewsApi failed");
                throw new Exception("Get request to HackerNewsApi failed");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw new Exception(ex.Message);
        }
    }
}