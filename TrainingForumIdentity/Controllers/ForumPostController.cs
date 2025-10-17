using Application.Service.Interface;
using EFCore;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingForumIdentity.Models;


namespace TrainingForumIdentity.Controllers
{    
    [Route("ForumPost")]
    public class ForumPostController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ForumPostController> _logger;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ISubCategoryService _subCategoryService;
        
        public ForumPostController(
            UserManager<User> userManager,
            IWebHostEnvironment env,
            ILogger<ForumPostController> logger,
            IPostService postService,
            ICommentService commentService,
            ISubCategoryService subCategoryService)
        {
            _userManager = userManager;
            _env = env;
            _logger = logger;
            _postService = postService;
            _commentService = commentService;
            _subCategoryService = subCategoryService;
        }

        [HttpGet("CreatePost")]
        public async Task<IActionResult> CreatePost()
        {
            // Build SubCategory select list from service
            var subcats = await _subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.SubCategories = subcats
                .OrderBy(sc => sc.Title)
                .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Title })
                .ToList();

            return View(new ForumPostViewModel());
        }

        [HttpPost("CreatePost")]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = 200 * 1024 * 1024)]
        public async Task<IActionResult> CreatePost(ForumPostViewModel vm)
        {
            void LoadSubCatsForReturn()
            {
                var items = _subCategoryService.GetAllSubCategoriesAsync().GetAwaiter().GetResult()
                    .OrderBy(sc => sc.Title)
                    .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Title })
                    .ToList();
                ViewBag.SubCategories = items;
            }

            if (!ModelState.IsValid)
            {
                LoadSubCatsForReturn();
                return View(vm);
            }

            // Validate SubCategory through service
            var subCat = await _subCategoryService.GetSubCategoryByIdAsync(vm.SubCategoryId);
            if (subCat == null)
            {
                ModelState.AddModelError(nameof(vm.SubCategoryId), "Välj en giltig underkategori.");
                LoadSubCatsForReturn();
                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            string? imagePath = null;
            if (vm.UploadedImage != null && vm.UploadedImage.Length > 0)
            {
                var ext = Path.GetExtension(vm.UploadedImage.FileName).ToLowerInvariant();
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError(nameof(vm.UploadedImage), "Endast jpg/jpeg/png/gif/webp tillåts.");
                    LoadSubCatsForReturn();
                    return View(vm);
                }

                var webRoot = _env.WebRootPath;
                if (string.IsNullOrWhiteSpace(webRoot))
                    webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var uploadDir = Path.Combine(webRoot, "userImage");
                Directory.CreateDirectory(uploadDir);

                var fileName = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadDir, fileName);
                using (var stream = System.IO.File.Create(fullPath))
                {
                    await vm.UploadedImage.CopyToAsync(stream);
                }
                imagePath = $"/userImage/{fileName}";
            }

            var post = new Post
            {
                Title = vm.Title?.Trim(),
                Content = vm.Content?.Trim(),
                SubCategoryId = vm.SubCategoryId,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                ImageUrl = imagePath
            };

            await _postService.CreatePostAsync(post);
            TempData["Success"] = "Inlägget skapades.";
            return RedirectToAction("ViewPost", new { id = post.Id });
        }

        [HttpGet("UpdatePost/{id:int}")]
        public async Task<IActionResult> UpdatePost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var subcats = await _subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.SubCategories = subcats
                .OrderBy(sc => sc.Title)
                .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Title })
                .ToList();

            var vm = new EditPostViewModel
            {
                Title = post.Title,
                Content = post.Content,
                SubCategoryId = post.SubCategoryId
            };
            return View(vm);
        }

        [HttpPost("UpdatePost/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(int id, EditPostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var subcats = await _subCategoryService.GetAllSubCategoriesAsync();
                ViewBag.SubCategories = subcats
                    .OrderBy(sc => sc.Title)
                    .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Title })
                    .ToList();
                return View(vm);
            }

            var subCat = await _subCategoryService.GetSubCategoryByIdAsync(vm.SubCategoryId);
            if (subCat == null)
            {
                ModelState.AddModelError(nameof(vm.SubCategoryId), "Välj en giltig underkategori.");
                var subcats = await _subCategoryService.GetAllSubCategoriesAsync();
                ViewBag.SubCategories = subcats
                    .OrderBy(sc => sc.Title)
                    .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Title })
                    .ToList();
                return View(vm);
            }

            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            post.Title = vm.Title?.Trim();
            post.Content = vm.Content?.Trim();
            post.SubCategoryId = vm.SubCategoryId;
            await _postService.UpdatePostAsync(id, post);

            TempData["Success"] = "Inlägget uppdaterades.";
            return RedirectToAction("ViewPost", new { id });
        }

        [HttpGet("ViewPost/{id:int}")]
        public async Task<IActionResult> ViewPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            var comments = await _commentService.GetAllCommentsByPostIdAsync(id);

            if (post.SubCategory == null)
            {
                post.SubCategory = await _subCategoryService.GetSubCategoryByIdAsync(post.SubCategoryId);
            }
            var vm = new PostCommentViewModel
            {
                Post = post,
                Comments = comments.OrderBy(c => c.CreatedAt).ToList()
            };


            return View(vm);
        }

        [HttpPost("AddComment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            // Ensure the post exists
            var post = await _postService.GetPostByIdAsync(postId);
            if (post == null) return NotFound();

            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Kommentaren kan inte vara tom.";
                return RedirectToAction("ViewPost", new { id = postId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var comment = new Comment
            {
                PostId = postId,
                UserId = user.Id,
                Content = content.Trim(),
                CreatedAt = DateTime.UtcNow,
                ParentCommentId = null
            };

            await _commentService.CreateCommentAsync(comment);
            TempData["Success"] = "Kommentar publicerad.";
            return RedirectToAction("ViewPost", new { id = postId });
        }

        [HttpPost("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            await _postService.DeletePostAsync(id);
            TempData["Success"] = "Inlägget raderades.";
            return RedirectToAction("Index", "Home");
        }
    }

}