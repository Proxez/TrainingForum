using Application.Service.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore;
public class ReportRepository : IReportRepository
{
    private readonly MyDbContext _context;

    public ReportRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<List<Report>> GetAllReportsAsync()
    {
        return await _context.Reports.ToListAsync();
    }
    public async Task<Report?> GetReportByIdAsync(int id)
    {
        return await _context.Reports.FindAsync(id);
    }
    public async Task AddReportAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateReportAsync(int id, Report updatedReport)
    {
        var report = await _context.Reports.FirstOrDefaultAsync(c => c.Id == id);
        if (report == null) return;

        report.Reason = updatedReport.Reason;
        report.Status = updatedReport.Status;
        report.ResolvedByUserId = updatedReport.ResolvedByUserId;
        report.ResolveAt = updatedReport.ResolveAt;

        await _context.SaveChangesAsync();

    }
    public async Task DeleteReportAsync(Report report)
    {
        if (report == null) return;

        _context.Reports.Remove(report);

        await _context.SaveChangesAsync();
    }
}
