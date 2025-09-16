using Entites;
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
        if (report != null)
        {
            report.Reason = updatedReport.Reason;
            _context.Reports.Update(updatedReport);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteReportAsync(int id)
    {
        var report = await _context.Reports.FindAsync(id);
        if (report != null)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }
    }
}
