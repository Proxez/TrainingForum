using Enums;
using System.ComponentModel.DataAnnotations;

namespace TrainingForumIdentity.Models;

public class CreateReportViewModel
{
    public ReactionTarget TargetType { get; set; }
    public int TargetId { get; set; }

    [Required, StringLength(500)]
    public string Reason { get; set; } = string.Empty;

    public string? TargetTitle { get; set; }
    public string? TargetPreview { get; set; }
}
