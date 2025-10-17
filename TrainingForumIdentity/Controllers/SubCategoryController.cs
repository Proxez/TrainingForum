using Application.Service.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TrainingForumIdentity.Models;

namespace TrainingForumIdentity.Controllers;
public class SubCategoryController : Controller
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ICategoryService _categoryService;

    public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> SubCategoryAdmin()
    {
        //var vm = new CategoryViewModel{ SubCategories = await _subCategoryService.GetAllSubCategoriesAsync()};
        var categories = await _categoryService.GetAllCategoriesAsync();
        var subCategories = await _subCategoryService.GetAllSubCategoriesAsync();
        var vm = categories.Select(c => new CategoryViewModel
        {
            Category = c,
            SubCategories = subCategories.Where(sc => sc.CategoryId == c.Id).ToList()
        }).ToList();
        return View(vm);
    }
    [HttpGet("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory()
    {
        var vm = new CategoryViewModel{ Categories = await _categoryService.GetAllCategoriesAsync()};
        return View(vm);
    }
    [HttpPost("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory(SubCategory subCategory)
    {
        await _subCategoryService.CreateSubCategoryAsync(subCategory);
        return RedirectToAction(nameof(SubCategoryAdmin));
    }
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        var category = await _subCategoryService.GetSubCategoryByIdAsync(id);
        await _subCategoryService.DeleteSubCategoryAsync(category.Id);
        return RedirectToAction(nameof(SubCategoryAdmin));
    }
    [HttpGet]
    public async Task<IActionResult> UpdateSubCategory(int id)
    {
        var subCategory = await _subCategoryService.GetSubCategoryByIdAsync(id);
        if (subCategory == null)
            return NotFound();

        var vm = new SubCategoryEditViewModel
        {
            Id = subCategory.Id,
            CategoryId = subCategory.CategoryId,
            Title = subCategory.Title,
            Description = subCategory.Description
        };

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateSubCategory(SubCategoryEditViewModel updatedSubCategory)
    {
        if (!ModelState.IsValid) return View(updatedSubCategory);

        var sub = await _subCategoryService.GetSubCategoryByIdAsync(updatedSubCategory.Id);

        if (sub == null) return NotFound();

        var subCategoryEdit = new SubCategory
        {
            Id = updatedSubCategory.Id,
            CategoryId = updatedSubCategory.CategoryId,
            Title = updatedSubCategory.Title,
            Description = updatedSubCategory.Description
        };
        await _subCategoryService.UpdateSubCategoryAsync(updatedSubCategory.Id, subCategoryEdit);

        return RedirectToAction(nameof(SubCategoryAdmin));

    }
    [HttpGet("SubCategory")]
    public async Task<IActionResult> SubCategory(int id)
    {
        var sub = await _subCategoryService.GetByIdWithPostsAsync(id);
        if (sub is null) return NotFound();

        sub.Posts = sub.Posts
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

        return View(sub); 
    }
}
