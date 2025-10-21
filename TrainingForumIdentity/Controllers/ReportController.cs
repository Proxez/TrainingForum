using Application.Service;
using Application.Service.Interface;
using EFCore;
using Entities;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainingForumIdentity.Models;

[Authorize]
public class ReportController : Controller
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }   

    [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
    [HttpGet("ViewReports")]
    public async Task<IActionResult> ViewReports()
    {
        var report = await _reportService.GetAllReportsAsync();
        if (report == null) return NotFound();
        return View(report);
    }

    [HttpPost("CreateReport")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateReport(CreateReportViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var report = new Report
        {
            ReporterUserId = GetUserId(),
            TargetType = vm.TargetType,
            TargetId = vm.TargetId,
            Reason = vm.Reason,
            Status = ReportStatus.Open,
            CreateAt = DateTimeOffset.UtcNow
        };

        await _reportService.CreateReportAsync(report);

        TempData["Message"] = "Tack! Din rapport har skickats.";
        return RedirectToAction("Index", "Home"); 
    }

    [HttpGet("CreateReport")]
    public IActionResult CreateReport(ReactionTarget targetType, int targetId)
    {
        var vm = new CreateReportViewModel { TargetType = targetType, TargetId = targetId };
        return View(vm); 
    }
    [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
    [HttpGet("UpdateReport")]
    public async Task<IActionResult> UpdateReport(int id)
    {
        var report = await _reportService.GetReportByIdAsync(id);
        if (report == null) return NotFound();
        return View(report);
    }
    
    [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
    [HttpPost("UpdateReport")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateReport(Report input)
    {
        if (!ModelState.IsValid) return View(input);

        var updated = new Report
        {
            Id = input.Id, 
            Reason = input.Reason,
            Status = input.Status,
            ResolvedByUserId = input.ResolvedByUserId,
            ResolveAt = input.ResolveAt?.ToUniversalTime()
        };

        await _reportService.UpdateReportAsync(input.Id, updated);
        return RedirectToAction(nameof(ViewReports));
    }
    
    [Authorize(Roles = $"{nameof(UserRole.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReport(int id)
    {
        await _reportService.DeleteReportAsync(id);
        return RedirectToAction("Index", "Home");
    }

    private int GetUserId()
    {
        var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(idStr)) throw new InvalidOperationException("User not signed in.");
        return int.Parse(idStr);
    }
}