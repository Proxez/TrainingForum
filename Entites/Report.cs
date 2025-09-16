using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites;
public class Report
{
    public int Id { get; set; }
    public int ReporterUserId { get; set; }
    public ReactionTarget TargetType { get; set; }
    public int TargetId { get; set; }
    public string Reason { get; set; }
    public ReportStatus Status { get; set; } = ReportStatus.Open;
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ResolveAt { get; set; }
    public int ResolveByUserId { get; set; }
    public User Reporter { get; set; }
    public User ResolvedBy { get; set; }
}
