
using System.ComponentModel;

namespace earfest.API.Domain.Entities;

public class Content : AbstractEntity, IAuditableEntity
{
    [DisplayName("İçerik")]
    public string Name { get; set; }
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    [DisplayName("Sözler")]
    public string? Lyrics { get; set; }
    [DisplayName("Sanatçılar")]
    public List<AppUser> Artists { get; set; }
    [DisplayName("Kategoriler")]
    public List<Category> Categories { get; set; }
    [DisplayName("Ruh halleri")]
    public List<Mood> Moods { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
