using Entities;

namespace Application.Service.Interface;
public interface ISubCategoryRepository
{
    Task AddSubCategoryAsync(SubCategory subCategory);
    Task DeleteSubCategoryAsync(SubCategory subCategory);
    Task<List<SubCategory>> GetAllSubCategoriesAsync();
    Task<SubCategory?> GetByIdWithPostsAsync(int id);
    Task<SubCategory?> GetSubCategoryByIdAsync(int id);
    Task UpdateSubCategoryAsync(int id, SubCategory updatedSubCategory);
}