using Application.Service.Interface;
using Entites;
using Microsoft.AspNetCore.Mvc;

namespace TrainingForumIdentity.Controllers;
public class ForumPostController : Controller
{
    private readonly IPostService _postService;

    public ForumPostController(IPostService postService)
    {
        _postService = postService;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet("CreatePost")]
    public IActionResult CreatePost()
    {
        return View();
    }
    [HttpPost("CreatePost")]
    public async Task<IActionResult> CreatePost(Post post)
    {
        await _postService.CreatePostAsync(post);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        await _postService.DeletePostAsync(post.Id);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet("UpdatePost")]
    public async Task<IActionResult> UpdatePost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        return View(post);
    }
    [HttpPost("UpdatePost")]
    public async Task<IActionResult> UpdatePost(Post updatedPost)
    {
        if(ModelState.IsValid)
        {
            await _postService.UpdatePostAsync(updatedPost.Id, updatedPost);
            return RedirectToAction(nameof(Index));
        }
        else
            return View(nameof(Index));
    }


}
