using Enums;

namespace TrainingForumIdentity.Models;

public class ReportListViewModel
{
    public int Id { get; set; }
    public ReactionTarget TargetType { get; set; }
    public int TargetId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public ReportStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string ReporterName { get; set; } = string.Empty;
}
