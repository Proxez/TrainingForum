using Application.Service;
using Application.Service.Interface;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrainingForumIdentity.Models;

namespace TrainingForumIdentity.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _serviceCategory;
    private readonly ISubCategoryService _serviceSubCategory;
    private readonly IPostService _servicePost;

    public CategoryController(ICategoryService serviceCategory, ISubCategoryService serviceSubCategory, IPostService servicePost)
    {
        _serviceCategory = serviceCategory;
        _serviceSubCategory = serviceSubCategory;
        _servicePost = servicePost;
    }
    public async Task<IActionResult> CategoryAdmin()
    {
        var categories = await _serviceCategory.GetAllCategoriesAsync();
        return View(categories);
    }
    [HttpGet("[action]")]
    public IActionResult CreateCategory()
    {
        return View();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);


        var category = new Category
        {
            Title = model.Title,
            Description = model.Description
        };

        await _serviceCategory.CreateCategoryAsync(category);
        return RedirectToAction(nameof(CategoryAdmin));
    }
    [HttpPost("[action]/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory([FromForm] int id)
    {
        try
        {
            await _serviceCategory.DeleteCategoryAsync(id);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("FK-conflict"))
        {
            TempData["Error"] = "Kategorin kan inte tas bort eftersom den används.";
        }
        catch (HttpRequestException)
        {
            TempData["Error"] = "Nätverksfel vid borttagning.";
        }

        return RedirectToAction(nameof(CategoryAdmin));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var category = await _serviceCategory.GetCategoryByIdAsync(id);
        return View(category);
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateCategory(Category updatedCategory)
    {
        if (ModelState.IsValid)
        {
            await _serviceCategory.UpdateCategoryAsync(updatedCategory.Id, updatedCategory);
            return RedirectToAction(nameof(CategoryAdmin));
        }
        else
            return View(nameof(CategoryAdmin));
    }
    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Category(int id)
    {
        Console.WriteLine($"[Forum] HIT Category/{id}");

        var categories = await _serviceCategory.GetAllCategoriesAsync() ?? new List<Category>();
        var subCategories = await _serviceSubCategory.GetAllSubCategoriesAsync() ?? new List<SubCategory>();
        var allPosts = await _servicePost.GetAllPostsAsync() ?? new List<Post>();

        Console.WriteLine("[Forum] Categories ids: " + string.Join(",", categories.Select(c => c.Id)));

        // 1) Försök hitta i listan
        var selectedCategory = categories.FirstOrDefault(c => c.Id == id);

        // 2) Fallback: hämta via /api/category/{id} om den saknas i listan
        if (selectedCategory is null)
        {
            Console.WriteLine($"[Forum] Fallback: GET by id={id}");
            selectedCategory = await _serviceCategory.GetCategoryByIdAsync(id);
        }

        // Filtrera poster om vi har en kategori
        var postsForCategory = new List<Post>();
        if (selectedCategory is not null)
        {
            var subIdsInCategory = subCategories.Where(sc => sc.CategoryId == id)
                                                .Select(sc => sc.Id)
                                                .ToHashSet();

            postsForCategory = allPosts
                .Where(p => p.SubCategoryId > 0 && subIdsInCategory.Contains(p.SubCategoryId))
                .OrderByDescending(p => p.CreatedAt == default ? DateTime.MinValue : p.CreatedAt)
                .ToList();


        }
        else
        {
            ViewData["Error"] = $"Kategorin med id={id} hittades inte i API:t.";
        }

        var vm = new CategoryViewModel
        {
            Category = selectedCategory,
            Categories = categories,
            SubCategories = subCategories,
            AllPosts = postsForCategory,
        };

        return View(vm);
    }
}
