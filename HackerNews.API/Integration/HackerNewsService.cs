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
        var uri = $"/v0/beststories.json";
        var resultContent = await SendRequestAsync(uri);
        return JsonConvert.DeserializeObject<List<long>>(resultContent);
    }

    public async Task<TopStoriesResponse> GetStoryByIdAsync(long id)
    {
        var uri = $"/v0/item/{id}.json";
        var resultContent = await SendRequestAsync(uri);
        var propertyParser = JsonConvert.DeserializeObject<StoryDetailResponse>(resultContent);

        if (propertyParser != null)
        {
            return new TopStoriesResponse
            {
                Title = propertyParser.Title,
                Uri = propertyParser.Url,
                PostedBy = propertyParser.By,
                Time = propertyParser.IsoTime,
                Score = propertyParser.Score,
                CommentCount = propertyParser.commentCounts
            };
        }
        return null;
    }

    private async Task<string> SendRequestAsync(string uri)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("HackerNewsApi");
            _logger.LogInformation($"Sending request to {uri}");
            var result = await httpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"Received response from {uri}: {resultContent}");
                return resultContent;
            }
            else
            {
                _logger.LogError($"Request to {uri} failed with status code {result.StatusCode}");
                throw new HttpRequestException($"Request to {uri} failed with status code {result.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in SendRequestAsync: {ex.Message}");
            throw;
        }
    }
}