namespace NotificationAPI.Models;

public record SendEmailRequest(string To, string Subject, string Body);