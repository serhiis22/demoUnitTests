using AutoMapper;
using MediatR;
using Serilog;
using Subscription.Application.Interfaces;
using Subscription.Application.Subscriptions.Queries.GetSubscription;

namespace Subscription.Application.Subscriptions.Queries.GetSubscriptionList;

public class SubscriptionListVm
{
    public IList<SubscriptionVm>? Subscriptions { get; set; }
}

public class GetSubscriptionListQuery : IRequest<SubscriptionListVm>
{
    public Guid EventId { get; set; }
}

public class GetSubscriptionListQueryHandler : IRequestHandler<GetSubscriptionListQuery, SubscriptionListVm>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionListQueryHandler(IMapper mapper, ISubscriptionRepository repository) => (_mapper, _repository) = (mapper, repository);
    
    public async Task<SubscriptionListVm> Handle(GetSubscriptionListQuery request, CancellationToken cancellationToken)
    {
        var entityDate = await _repository.GetSubscriptionsByEventId(request.EventId, cancellationToken);
        var entity = _mapper.Map<IList<SubscriptionVm>>(entityDate);

        Log.Information("Subscription List: {@Entity}", entity);
        return new SubscriptionListVm { Subscriptions = entity };
    }
}