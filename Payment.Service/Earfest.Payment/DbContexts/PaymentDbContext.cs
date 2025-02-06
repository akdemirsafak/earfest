using Earfest.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Earfest.Payment.DbContexts;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentEntity> Payments { get; set; }
}
