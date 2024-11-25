﻿using System.Net;
using System.Net.Mail;

namespace earfest.API.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
        {
            Port = int.Parse(_configuration["Smtp:Port"]),
            Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
            EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]),
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Smtp:From"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
