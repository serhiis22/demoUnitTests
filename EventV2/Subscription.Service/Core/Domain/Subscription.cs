namespace Subscription.Domain;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string? Email { get; set; }
    public ICollection<SubscriptionsDate>? Dates { get; set; }
}

public class SubscriptionsDate
{
    public int Id { get; set; }
    public Guid SubscriptionId { get; set; }
    public TimeSpan NotificationTime { get; set; }
    public Subscription Subscription { get; set; }
}