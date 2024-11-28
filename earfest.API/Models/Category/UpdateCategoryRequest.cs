namespace earfest.API.Models.Category;

public record UpdateCategoryRequest(string Name, string? Description, string? ImageUrl);