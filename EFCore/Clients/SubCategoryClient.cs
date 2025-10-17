using Application.Service.DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFCore.Clients;
public class SubCategoryClient : ISubCategoryClient
{
    private readonly HttpClient _subCategoryClient;

    public SubCategoryClient(HttpClient subCategoryClient)
    {
        _subCategoryClient = subCategoryClient;
    }
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync(string uri)
    {
        var subCategoriesDto = new List<SubCategoryDto>();

        HttpResponseMessage response = await _subCategoryClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            subCategoriesDto = JsonSerializer.Deserialize<List<SubCategoryDto>>(responseString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        var subCategories = from c in subCategoriesDto
                            select new SubCategory()
                            {
                                Title = c.Title,
                                Description = c.Description
                            };

        return subCategories.ToList();
    }

    public Task<SubCategory?> GetByIdWithPostsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<SubCategory> GetCategoryByIdAsync(string uri)
    {
        var subCategoryDto = new SubCategoryDto();

        HttpResponseMessage response = await _subCategoryClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            subCategoryDto = JsonSerializer.Deserialize<SubCategoryDto>(responseString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        var subCategory = new SubCategory()
        {
            Title = subCategoryDto.Title,
            Description = subCategoryDto.Description
        };

        return subCategory;
    }
}
