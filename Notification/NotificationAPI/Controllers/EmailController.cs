using earfest.Shared.Base;
using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Models;
using NotificationAPI.Services;

namespace NotificationAPI.Controllers;

public class EmailController : EarfestBaseController
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailRequest request)
    {
        await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
        return Ok();
    }
}
