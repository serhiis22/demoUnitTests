using Microsoft.EntityFrameworkCore;

namespace Subscription.Persistence;

public class DbInitializer
{
    public static void Initialize(SubscriptionDbContext context)
    {
        context.Database.Migrate();
    }
}