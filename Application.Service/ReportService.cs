using Application.Service.Interface;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return new List<Report>();
    }
    public async Task<Report> GetReportByIdAsync(int id)
    {
        return new Report();
    }
    public async Task CreateReportAsync(Report report)
    {
        // Logic to create a new report
    }
    public async Task UpdateReportAsync(int id, Report report)
    {
        // Logic to update an existing report
    }
    public async Task DeleteReportAsync(int id)
    {
        // Logic to delete a report
    }

}
