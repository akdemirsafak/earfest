namespace Earfest.Auth.Helpers;

public interface ICurrentUser
{
    string GetUserId { get; }
    string GetEmail { get; }
    string GetUserName { get; }
}
