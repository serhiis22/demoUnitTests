using Microsoft.EntityFrameworkCore;
using Subscription.Persistence.SubscriptionTypeConfigurations;
using Subscriptions.Application.Interfaces;

namespace Subscription.Persistence;

public class SubscriptionDbContext : DbContext, ISubscriptionDbContext
{
    public DbSet<Domain.Subscription> Subscriptions { get; set; }
    public DbSet<Domain.SubscriptionsDate> SubscriptionDates { get; set; }

    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SubscriptionConfigurations());
        builder.ApplyConfiguration(new SubscriptionDateConfigurations());
        base.OnModelCreating(builder);
    }
}