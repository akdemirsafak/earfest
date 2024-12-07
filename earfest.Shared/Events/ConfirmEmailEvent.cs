namespace earfest.Shared.Events;

public class ConfirmEmailEvent
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
