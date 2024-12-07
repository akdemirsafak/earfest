using System.Net.Mail;

namespace Notification.Service.Models;

public class EmailDto
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<Attachment> Attachments { get; set; } = null;
}
