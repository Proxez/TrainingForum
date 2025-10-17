using Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;
public class Report
{
    public int Id { get; set; }
    public int ReporterUserId { get; set; }
    public ReactionTarget TargetType { get; set; }
    public int TargetId { get; set; }
    [Required, MaxLength(500)]
    public string Reason { get; set; } = "";
    public ReportStatus Status { get; set; } = ReportStatus.Open;
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ResolveAt { get; set; }
    public int? ResolvedByUserId { get; set; }
    [ValidateNever]
    public User Reporter { get; set; } = default!;
    [ValidateNever]
    public User? ResolvedBy { get; set; }
}
