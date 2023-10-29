using Subscription.Application.Subscriptions.Command.UpdateSubscription;
using Subscription.Application.Subscriptions.Queries.GetSubscription;

namespace Subscription.Application.Interfaces;

public interface ISubscriptionRepository
{
    public Task<IList<Domain.Subscription>> GetAllSubscriptions(CancellationToken cancellationToken);
    public Task AddSubscriptions(Domain.Subscription model, CancellationToken cancellationToken);
    public Task UpdateSubscriptions(UpdateSubscriptionCommand model, CancellationToken cancellationToken);
    Task<Domain.Subscription> GetSubscriptionsById(Guid id, CancellationToken cancellationToken);
    Task<IList<Domain.Subscription>> GetSubscriptionsByEventId(Guid eventId, CancellationToken cancellationToken);
    public Task RemoveSubscriptions(Guid eventId, CancellationToken cancellationToken);
    public Task<Domain.Subscription> GetSubscriptionsByQuery(GetSubscriptionQuery query, CancellationToken cancellationToken);
}