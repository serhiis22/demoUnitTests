using Microsoft.EntityFrameworkCore;
using Subscription.Domain;

namespace Subscriptions.Application.Interfaces
{
    public interface ISubscriptionDbContext
    {
        public DbSet<Subscription.Domain.Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionsDate> SubscriptionDates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
