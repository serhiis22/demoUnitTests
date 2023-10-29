namespace Subscription.Application.Interfaces;

public interface ISubscriptionService
{
    Task CreateNotifications(Domain.Subscription subscription);
    Task RemoveNotifications(Guid eventId);
}