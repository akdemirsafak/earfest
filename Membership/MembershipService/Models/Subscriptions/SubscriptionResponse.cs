namespace MembershipService.Models.Subscriptions;

public class SubscriptionResponse
{
    public string Id { get; set; }
    public string PlanId { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Statu { get; set; }
    public int StatuId { get; set; }
}
