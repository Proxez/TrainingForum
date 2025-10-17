using Application.Service.DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EFCore.Clients;
public class CategoryClient : ICategoryClient
{
    private readonly HttpClient _categoryClient;

    public CategoryClient(HttpClient categoryClient)
    {
        _categoryClient = categoryClient;
    }
    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };
    public async Task<List<Category>> GetAllCategoriesAsync(string uri)
    {

        var categoriesDto = new List<CategoryDto>();

        using HttpResponseMessage response = await _categoryClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            categoriesDto = JsonSerializer.Deserialize<List<CategoryDto>>(
                responseString,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            ) ?? new List<CategoryDto>();
        }
        else
        {
            return new List<Category>();
        }

        var categories =
            from c in categoriesDto
            select new Category
            {
                Id = c.Id,                 
                Title = c.Title,
                Description = c.Description
            };

        return categories.ToList();
    }
    public async Task<Category> GetCategoryByIdAsync(string uri)
    {
        HttpResponseMessage response = await _categoryClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        if (!response.IsSuccessStatusCode)
            return null; 

        string responseString = await response.Content.ReadAsStringAsync();

        CategoryDto? dto;
        try
        {
            dto = JsonSerializer.Deserialize<CategoryDto>(responseString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        catch (JsonException)
        {
            return null;
        }

        if (dto == null)
            return null;

        return new Category
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description
        };
    }
    public async Task<CategoryDto> CreateCategoryAsync(Category category, CancellationToken ct = default)
    {
        var req = new CreateCategoryDto { Title = category.Title, Description = category.Description };

        using var resp = await _categoryClient.PostAsJsonAsync("api/Category", req, _json, ct);

        var body = await resp.Content.ReadAsStringAsync(ct);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException($"Create failed: {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {body}");

        var dto = JsonSerializer.Deserialize<CategoryDto>(body, _json);
        if (dto is null || dto.Id <= 0)
            throw new InvalidOperationException($"API returned invalid CategoryDto (Id <= 0). Body: {body}");
        return dto;
    }
    public async Task<Category?> GetCategoryWithSubcatsAndPostsAsync(int id)
    {
        return await GetCategoryByIdAsync($"api/category/{id}");
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        var body = new
        {
            title = category.Title,
            description = category.Description
        };

        string json = JsonSerializer.Serialize(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _categoryClient.PutAsync($"api/category/{id}", content);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> DeleteCategoryAsync(int id, CancellationToken ct = default)
    {
        using HttpResponseMessage response = await _categoryClient.DeleteAsync($"api/category/{id}", ct);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return false; 

        if(response.StatusCode == HttpStatusCode.Conflict)
            throw new InvalidOperationException("Kategori används redan(FK-konflikt)");

        response.EnsureSuccessStatusCode();
        return true;
    }
}
