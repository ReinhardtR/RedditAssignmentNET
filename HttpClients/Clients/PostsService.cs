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

    public async Task<ICollection<PostBasicDto>> GetAllPostsAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("https://localhost:7212/posts");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(content);


        Console.WriteLine("CONTENT: " + content);
        ICollection<PostBasicDto>? posts = JsonSerializer.Deserialize<ICollection<PostBasicDto>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return posts ?? new List<PostBasicDto>();
    }

    public async Task<PostFullDto> GetPostByIdAsync(string id)
    {
        HttpResponseMessage response = await _client.GetAsync($"https://localhost:7212/posts/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(content);

        PostFullDto? post = JsonSerializer.Deserialize<PostFullDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (post == null) throw new Exception("Something went wrong");

        return post;
    }

    public async Task<PostBasicDto> CreatePostAsync(PostCreateDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("https://localhost:7212/posts", dto);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(content);

        PostBasicDto? post = JsonSerializer.Deserialize<PostBasicDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (post == null) throw new Exception("Something went wrong");

        return post;
    }
}