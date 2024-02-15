using HackerNews.API.Integration;
using HackerNews.API.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

public class HackerNewsServiceTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly Mock<ILogger<HackerNewsService>> _mockLogger;
    private readonly Mock<IOptions<HackerApiOption>> _mockOptions;
    private readonly HackerNewsService _hackerNewsService;

    public HackerNewsServiceTests()
    {
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        _mockLogger = new Mock<ILogger<HackerNewsService>>();
        _mockOptions = new Mock<IOptions<HackerApiOption>>();
        _mockOptions.Setup(o => o.Value).Returns(new HackerApiOption { BestStories = "https://hacker-news.firebaseio.com/v0/beststories.json" });

        _hackerNewsService = new HackerNewsService(_mockHttpClientFactory.Object, _mockLogger.Object, _mockOptions.Object);
    }

    [Fact]
    public async Task GetBestStoriesAsync_ReturnsList_WhenServiceSucceeds()
    {
        // Arrange
        var httpClient = new HttpClient();
        _mockHttpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);
        // Act
        var result = await _hackerNewsService.GetBestStoriesAsync();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetBestStoriesAsync_WhenOptionsNotInitialized_ThrowsException()
    {
        // Arrange
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        var mockLogger = new Mock<ILogger<HackerNewsService>>();
        var mockOptions = new Mock<IOptions<HackerApiOption>>();

        var service = new HackerNewsService(mockHttpClientFactory.Object, mockLogger.Object, mockOptions.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => service.GetBestStoriesAsync());
    }
}
