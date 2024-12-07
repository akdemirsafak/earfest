namespace earfest.Shared.Events;

public class PasswordChangedEmailEvent
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
