namespace earfest.API.Models.Plans;

public record UpdatePlanRequest(string Name, string? Description, decimal Price, int Duration, bool IsTrial, bool IsFree, bool IsPremium);
