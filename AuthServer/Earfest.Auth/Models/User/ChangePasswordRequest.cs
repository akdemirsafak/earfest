namespace Earfest.Auth.Models.User;

public sealed record ChangePasswordRequest(string Email, string CurrentPassword, string NewPassword);
