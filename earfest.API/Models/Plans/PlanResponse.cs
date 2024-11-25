namespace earfest.API.Models.Plans;

public class PlanResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public bool IsTrial { get; set; }
    public bool IsFree { get; set; }
    public bool IsPremium { get; set; }
}
