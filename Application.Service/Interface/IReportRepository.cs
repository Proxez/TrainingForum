using Entites;

namespace Application.Service.Interface;
public interface IReportRepository
{
    Task AddReportAsync(Report report);
    Task DeleteReportAsync(int id);
    Task<List<Report>> GetAllReportsAsync();
    Task<Report?> GetReportByIdAsync(int id);
    Task UpdateReportAsync(int id, Report report);
}