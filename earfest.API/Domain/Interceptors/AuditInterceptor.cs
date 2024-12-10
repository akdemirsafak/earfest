﻿using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace earfest.API.Domain.Interceptors;

public sealed class AuditInterceptor : SaveChangesInterceptor
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        string? userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            dbContext
            .ChangeTracker
                .Entries<IAuditableEntity>();
        foreach (EntityEntry<IAuditableEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.CreatedBy).CurrentValue = userId;

            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.UpdatedBy).CurrentValue = userId;
            }
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Property(x => x.DeletedAt).CurrentValue = DateTime.UtcNow;
                entry.Entity.IsDeleted = true;
                entry.Property(x => x.DeletedBy).CurrentValue = userId;
            }

        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}