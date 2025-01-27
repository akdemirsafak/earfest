namespace Earfest.Auth.Models.User;

public sealed record UpdateUserRequest(string FirstName, string LastName, DateTime? BirthDate);