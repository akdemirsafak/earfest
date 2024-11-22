
using System.ComponentModel;

namespace earfest.API.Domain.Entities;

public class Plan : AbstractEntity, IAuditableEntity
{
    [DisplayName("Plan")]
    public string Name { get; set; }
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    [DisplayName("Fiyat")]
    public decimal Price { get; set; }
    [DisplayName("Süre")]
    public int Duration { get; set; }
    [DisplayName("Deneme")]
    public bool IsTrial { get; set; }
    [DisplayName("Ücretsiz")]
    public bool IsFree { get; set; }
    [DisplayName("Premium")]
    public bool IsPremium { get; set; }
    [DisplayName("Üyeler")]
    public virtual List<AppUser> Subscribers { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
