namespace earfest.API.Domain.Entities;

public class Plan : AbstractEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; } = 30;
    public bool IsTrial { get; set; }
    public bool IsFree { get; set; }
    public bool IsPremium { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
