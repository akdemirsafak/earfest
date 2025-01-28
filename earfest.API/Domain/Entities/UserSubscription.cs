namespace earfest.API.Domain.Entities;

public class UserSubscription
{
    public UserSubscription()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public DateTime StartDate { get; set; } // Üyelik başlangıç tarihi
    public DateTime EndDate { get; set; } // Üyelik bitiş tarihi
    public bool IsActive { get; set; } // Üyelik aktif mi?
    public PaymentStatus PaymentStatus { get; set; } // Ödeme durumu: Başarılı, Bekliyor, Hatalı
    //public virtual AppUser User { get; set; }
    public string UserId { get; set; } // Identity kullanıcısı ile ilişkilendirme
    public virtual Plan Plan { get; set; }
    public string PlanId { get; set; } // Plan ile ilişkilendirme
   
}
public enum PaymentStatus
{
    Success,
    Pending,
    Failed
}
