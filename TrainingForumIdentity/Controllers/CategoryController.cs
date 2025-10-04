using Application.Service.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using TrainingForumIdentity.Models;

namespace TrainingForumIdentity.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }
    public async Task<IActionResult> CategoryAdmin()
    {
        var categories = await _service.GetAllCategoriesAsync();
        return View(categories);
    }
    [HttpGet("CreateCategory")]
    public IActionResult CreateCategory()
    {
        return View();
    }
    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        await _service.CreateCategoryAsync(category);
        return RedirectToAction(nameof(CategoryAdmin));
    }
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _service.GetCategoryByIdAsync(id);
        await _service.DeleteCategoryAsync(category.Id);
        return RedirectToAction(nameof(CategoryAdmin));
    }
    [HttpGet("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var category = await _service.GetCategoryByIdAsync(id);
        return View(category);
    }
    [HttpPost("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(Category updatedCategory)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateCategoryAsync(updatedCategory.Id, updatedCategory);
            return RedirectToAction(nameof(CategoryAdmin));
        }
        else
            return View(nameof(CategoryAdmin));
    }
    [HttpGet("Category")]
    public async Task<IActionResult> Category(int id)
    {
        var categories = new CategoryViewModel { Categories = await _service.GetAllCategoriesAsync() };
        Category sender = new();
        foreach(var category in categories.Categories)
        {
            if(category.Id == id)
            {
                sender = category;
            }
        }
        return View(sender);
    }
}
