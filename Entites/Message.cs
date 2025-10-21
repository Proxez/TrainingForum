using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;
public class Message
{
    public int Id { get; set; }

    [Required]
    public int SenderId { get; set; }

    [Required]
    public int RecipientId { get; set; }

    [Required, MaxLength(4000)]
    public string Body { get; set; } = string.Empty;

    public DateTimeOffset SentAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public bool IsRead { get; set; }

    // Navigationer
    public User? Sender { get; set; }
    public User? Recipient { get; set; }
}
