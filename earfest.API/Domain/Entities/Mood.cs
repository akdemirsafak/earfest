﻿
using System.ComponentModel;

namespace earfest.API.Domain.Entities;

public class Mood : AbstractEntity, IAuditableEntity
{
    [DisplayName("Ruh hali")]
    public string Name { get; set; }
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    [DisplayName("İçerikler")]
    public virtual IList<Content> Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
