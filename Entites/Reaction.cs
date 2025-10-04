using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;
public class Reaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ReactionTarget TargetType { get; set; }
    public int TargetId { get; set; } 
    public ReactionType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


    public User User { get; set; } = null!;
}
