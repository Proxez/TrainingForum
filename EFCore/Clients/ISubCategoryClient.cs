using Entities;

namespace EFCore.Clients;
public interface ISubCategoryClient
{
    Task<List<SubCategory>> GetAllSubCategoriesAsync(string uri);
    Task<SubCategory> GetCategoryByIdAsync(string uri);
    Task<SubCategory?> GetByIdWithPostsAsync(int id);
}