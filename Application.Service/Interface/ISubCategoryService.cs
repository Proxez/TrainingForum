using Entites;

namespace Application.Service.Interface;
public interface ISubCategoryService
{
    Task CreateSubCategoryAsync(SubCategory subCategory);
    Task DeleteSubCategoryAsync(int id);
    Task<List<SubCategory>> GetAllSubCategoriesAsync();
    Task<SubCategory> GetSubCategoryByIdAsync(int id);
    Task UpdateSubCategoryAsync(int id, SubCategory subCategory);
}