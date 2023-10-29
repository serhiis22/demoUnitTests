using Microsoft.EntityFrameworkCore;
using Subscription.Application.Interfaces;
using Subscription.Application.Subscriptions.Command.UpdateSubscription;
using Subscription.Application.Subscriptions.Queries.GetSubscription;
using Subscriptions.Application.Interfaces;

namespace Subscription.Persistence;

public class SubscriptionRepository : ISubscriptionRepository
{

    private readonly ISubscriptionDbContext _context;

    public SubscriptionRepository(ISubscriptionDbContext context)
    {
        _context = context;
    }
    public async Task<IList<Domain.Subscription>> GetAllSubscriptions(CancellationToken cancellationToken)
    {
        return await _context.Subscriptions.ToListAsync();
    }

    public async Task AddSubscriptions(Domain.Subscription model, CancellationToken cancellationToken)
    {
        await _context.Subscriptions.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSubscriptions(UpdateSubscriptionCommand model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Subscription> GetSubscriptionsById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Domain.Subscription>> GetSubscriptionsByEventId(Guid eventId, CancellationToken cancellationToken)
    {
        var result = await _context.Subscriptions.Where(model =>
            model.EventId.Equals(eventId)).Include(s=>s.Dates).ToListAsync(cancellationToken); ;

        return result;
    }

    public async Task RemoveSubscriptions(Guid eventId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Subscription> GetSubscriptionsByQuery(GetSubscriptionQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}