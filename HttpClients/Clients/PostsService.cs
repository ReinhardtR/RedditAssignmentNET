using System.Net.Http.Json;
using System.Text.Json;
using Domain.Dtos;
using HttpClients.Interfaces;

namespace HttpClients.Clients;

public class PostsService : IPostsService
{
    private readonly HttpClient _client;

    public PostsService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<PostBasicDto>> GetAllPostsAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("https://localhost:7212/posts");

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");

        string content = await response.Content.ReadAsStringAsync();
        IEnumerable<PostBasicDto>? posts = JsonSerializer.Deserialize<IEnumerable<PostBasicDto>>(content);

        return posts ?? Enumerable.Empty<PostBasicDto>();
    }

    public async Task<PostFullDto> GetPostByIdAsync(string id)
    {
        HttpResponseMessage response = await _client.GetAsync($"https://localhost:7212/posts/{id}");
        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");

        string content = await response.Content.ReadAsStringAsync();
        PostFullDto? post = JsonSerializer.Deserialize<PostFullDto>(content);

        if (post == null) throw new Exception("Something went wrong");

        return post;
    }

    public async Task<PostBasicDto> CreatePostAsync(PostCreateDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("https://localhost:7212/posts", dto);

        if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");

        string content = await response.Content.ReadAsStringAsync();
        PostBasicDto? post = JsonSerializer.Deserialize<PostBasicDto>(content);

        if (post == null) throw new Exception("Something went wrong");

        return post;
    }
}