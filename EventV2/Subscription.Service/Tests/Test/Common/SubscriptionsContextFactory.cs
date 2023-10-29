using Microsoft.EntityFrameworkCore;
using Subscription.Domain;
using Subscription.Persistence;

namespace Subscription.Test.Common;

public class SubscriptionsContextFactory
    {
        public static SubscriptionDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SubscriptionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SubscriptionDbContext(options);
            context.Database.EnsureCreated();

            var subscription1 = new Domain.Subscription
            {
                Id = Guid.NewGuid(),
                EventId = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                Email = "user1@example.com"
            };

            var subscription2 = new Domain.Subscription
            {
                Id = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                Email = "user2@example.com"
            };

            var subscription3 = new Domain.Subscription
            {
                Id = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                Email = "user3@example.com"
            };

            subscription1.Dates = new List<SubscriptionsDate>
            {
                new SubscriptionsDate
                {
                    Id = 1,
                    SubscriptionId = subscription1.Id,
                    NotificationTime = TimeSpan.FromHours(2),
                    Subscription = subscription1
                },
                new SubscriptionsDate
                {
                    Id = 2,
                    SubscriptionId = subscription1.Id,
                    NotificationTime = TimeSpan.FromHours(4),
                    Subscription = subscription1
                }
            };

            subscription2.Dates = new List<SubscriptionsDate>
            {
                new SubscriptionsDate
                {
                    Id = 3,
                    SubscriptionId = subscription2.Id,
                    NotificationTime = TimeSpan.FromHours(1),
                    Subscription = subscription2
                }
            };

            subscription3.Dates = new List<SubscriptionsDate>
            {
                new SubscriptionsDate
                {
                    Id = 4,
                    SubscriptionId = subscription3.Id,
                    NotificationTime = TimeSpan.FromHours(3),
                    Subscription = subscription3
                },
                new SubscriptionsDate
                {
                    Id = 5,
                    SubscriptionId = subscription3.Id,
                    NotificationTime = TimeSpan.FromHours(6),
                    Subscription = subscription3
                },
                new SubscriptionsDate
                {
                    Id = 6,
                    SubscriptionId = subscription3.Id,
                    NotificationTime = TimeSpan.FromHours(9),
                    Subscription = subscription3
                }
            };
            context.Subscriptions.AddRange(subscription1, subscription2, subscription3);

            context.SaveChanges();
            return context;
        }

        public static void Destroy(SubscriptionDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }