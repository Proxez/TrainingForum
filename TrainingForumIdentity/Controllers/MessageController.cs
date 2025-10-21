using Application.Service.Interface;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TrainingForumIdentity.Controllers;

[Authorize]
public class MessageController : Controller
{
    private readonly IMessageService _service;
    private readonly UserManager<User> _userManager;

    public MessageController(IMessageService service, UserManager<User> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    public async Task<IActionResult> Inbox(int page = 1, int pageSize = 50)
    {
        var me = await _userManager.GetUserAsync(User);
        if (me == null) return Challenge();

        var skip = Math.Max(0, (page - 1) * pageSize);
        var (items, unread) = await _service.GetInboxAsync(me.Id, skip, pageSize);

        ViewBag.Unread = unread;
        return View(items);
    }

    public async Task<IActionResult> Thread(int userId)
    {
        var me = await _userManager.GetUserAsync(User);
        if (me == null) return Challenge();

        var msgs = await _service.GetThreadAsync(me.Id, userId);
        ViewBag.OtherUserId = userId;
        return View(msgs);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Send(int recipientId, string body, string returnUrl = "")
    {
        var me = await _userManager.GetUserAsync(User);
        if (me == null) return Challenge();

        if (recipientId <= 0 || string.IsNullOrWhiteSpace(body))
        {
            TempData["Error"] = "Mottagare och meddelande krävs.";
            return RedirectToAction(nameof(Thread), new { userId = recipientId });
        }

        await _service.SendAsync(me.Id, recipientId, body);
        return Redirect(string.IsNullOrWhiteSpace(returnUrl)
            ? Url.Action(nameof(Thread), new { userId = recipientId })!
            : returnUrl);
    }

    [HttpGet]
    public IActionResult Create(int? recipientId = null)
    {
        ViewBag.RecipientId = recipientId;
        return View();
    }
}
