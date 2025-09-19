using Application.Service.Interface;
using Entites;
using Microsoft.AspNetCore.Mvc;

namespace TrainingForum.Web.Controllers;
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
    public IActionResult CreateCategory(Category category)
    {
        _service.CreateCategoryAsync(category);
        return RedirectToAction(nameof(CategoryAdmin));
    }
    public IActionResult DeleteCategory(int id)
    {
        return View();
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
}
