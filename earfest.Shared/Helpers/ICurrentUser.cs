namespace earfest.Shared.Helpers;
public interface ICurrentUser
{
    string GetUserId { get; }
    string GetEmail { get; }
    string GetUserName { get; }
}
