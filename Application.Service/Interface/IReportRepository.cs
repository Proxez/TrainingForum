
using Entities;

namespace Application.Service.Interface;
public interface IReportRepository
{
    Task AddReportAsync(Report report);
    Task DeleteReportAsync(Report report);
    Task<List<Report>> GetAllReportsAsync();
    Task<Report?> GetReportByIdAsync(int id);
    Task UpdateReportAsync(int id, Report report);
}