using Application.Service.Interface;
using Entites;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrainingForum.Web.Models;

namespace TrainingForum.Web.Controllers;
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
    [HttpGet("UpdateSubCategory")]
    public async Task<IActionResult> UpdateSubCategory(int id)
    {
        var category = await _subCategoryService.GetSubCategoryByIdAsync(id);
        return View(category);
    }
    [HttpPost("UpdateSubCategory")]
    public async Task<IActionResult> UpdateSubCategory(SubCategory updatedSubCategory)
    {
        if (ModelState.IsValid)
        {
            await _subCategoryService.UpdateSubCategoryAsync(updatedSubCategory.Id, updatedSubCategory);
            return RedirectToAction(nameof(SubCategoryAdmin));
        }
        else
            return View(nameof(SubCategoryAdmin));
    }
    [HttpGet("SubCategory")]
    public async Task<IActionResult> SubCategory(int id)
    {
        var subCategories = new CategoryViewModel { SubCategories = await _subCategoryService.GetAllSubCategoriesAsync() };
        SubCategory sender = new();
        foreach (var subCategory in subCategories.SubCategories)
        {
            if (subCategory.Id == id)
            {
                sender = subCategory;
            }
        }
        return View(sender);
    }
}
