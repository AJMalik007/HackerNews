using HackerNews.API.Integration;
using HackerNews.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("HackerNewsApi", client =>
{
    //TODO: Add the base address for the HackerNewsApi
    client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
});
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
