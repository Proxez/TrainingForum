using Microsoft.AspNetCore.Identity.UI.Services;

namespace TrainingForumIdentity.Areas.Identity.Pages.Account.Manage;


public sealed class NullEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
        => Task.CompletedTask;
}

