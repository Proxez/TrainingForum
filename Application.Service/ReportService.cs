using Application.Service.Interface;
using Entities;

namespace Application.Service;
public class ReportService : IReportService
{
    private readonly IReportRepository _repo;

    public ReportService(IReportRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Report>> GetAllReportsAsync()
    {
        return await _repo.GetAllReportsAsync();
    }
    public async Task<Report> GetReportByIdAsync(int id)
    {
        return await _repo.GetReportByIdAsync(id);
    }
    public async Task CreateReportAsync(Report report)
    {
        await _repo.AddReportAsync(report);
    }
    public async Task UpdateReportAsync(int id, Report report)
    {
        await _repo.UpdateReportAsync(id, report);
    }
    public async Task DeleteReportAsync(int id)
    {
        var report = await _repo.GetReportByIdAsync(id);
        await _repo.DeleteReportAsync(report);
    }

}
