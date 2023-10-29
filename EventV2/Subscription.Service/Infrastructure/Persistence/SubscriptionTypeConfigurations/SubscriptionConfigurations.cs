using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Subscription.Persistence.SubscriptionTypeConfigurations;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Domain.Subscription>
{
    public void Configure(EntityTypeBuilder<Domain.Subscription> builder)
    {
        builder.HasKey(ev => ev.Id);
        builder.HasIndex(ev => ev.Id).IsUnique();
        builder.HasIndex(s => s.EventId);
        builder.Property(e => e.EventId).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(255);
        builder.HasMany(e => e.Dates)
            .WithOne(d => d.Subscription)
            .HasForeignKey(d => d.SubscriptionId);
    }
}

public class SubscriptionDateConfigurations : IEntityTypeConfiguration<Domain.SubscriptionsDate>
{
    public void Configure(EntityTypeBuilder<Domain.SubscriptionsDate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.SubscriptionId).IsRequired();
        builder.Property(e => e.NotificationTime).IsRequired();
    }
}