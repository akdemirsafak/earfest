namespace MembershipService.Entities;

public class Subscription
{
    public Subscription()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string UserId { get; set; } // Identity kullanıcısı ile ilişkilendirme
    public string PlanId { get; set; } // Plan ile ilişkilendirme
    public DateTime StartDate { get; set; } // Üyelik başlangıç tarihi
    public DateTime EndDate { get; set; } // Üyelik bitiş tarihi
    public SubscriptionStatu Statu { get; set; } // Üyelik aktif mi? Aktif,iptal edildi, süresi doldu
    public string PaymentId { get; set; } // Ödeme ile ilişkilendirme
}
public enum SubscriptionStatu
{
    Active,
    Canceled,
    Expired,
    Pending
}
