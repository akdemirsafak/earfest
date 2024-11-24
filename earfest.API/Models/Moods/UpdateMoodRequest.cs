namespace earfest.API.Models.Moods;

public record UpdateMoodRequest(string Name, string? Description, string? ImageUrl);