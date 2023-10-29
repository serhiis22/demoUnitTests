using AutoMapper;
using MediatR;
using Subscription.Application.Common.Exceptions;
using Subscription.Application.Common.Mapping;
using Subscription.Application.Interfaces;
using Subscription.Domain;

namespace Subscription.Application.Subscriptions.Queries.GetSubscription;

public class SubscriptionVm : IMapWith<Domain.Subscription>
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string? Email { get; set; }
    public IList<long>? Dates { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SubscriptionVm, Domain.Subscription>().ForMember(
            dest => dest.Dates,
            opt => opt.MapFrom(src => new LongToTimeSpanListConverter().Convert(src.Dates, null))
        );

        //profile.CreateMap<Domain.Subscription, SubscriptionVm>().ForMember(
        //    dest => dest.Dates,
        //    opt => 
        //        opt.MapFrom(src => new SubscriptionsDateListToLongConverter().Convert((List<SubscriptionsDate>)src.Dates, null))
        //);
        profile.CreateMap<Domain.Subscription, SubscriptionVm>().ForMember(
                dest => dest.Dates,
                opt => opt.MapFrom(src => src.Dates != null ? src.Dates.Select(d => d.NotificationTime.Ticks).ToList() : new List<long>())

            );
    }
}


public class SubscriptionsDateListToLongConverter : IValueConverter<List<SubscriptionsDate>, List<long>>
{
    public List<long> Convert(List<SubscriptionsDate> sourceMember, ResolutionContext context)
    {
        if (sourceMember == null) return null;

        return sourceMember.Select(d => d.NotificationTime.Ticks).ToList();
    }
}


public class GetSubscriptionQuery : IRequest<SubscriptionVm>
{
    public Guid EventId { get; set; }
    public string? Email { get; set; }
}

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, SubscriptionVm>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SubscriptionVm> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetSubscriptionsByQuery(request, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Subscription), request.EventId);
        }

        return _mapper.Map<SubscriptionVm>(entity);
    }
}

public class LongToTimeSpanListConverter : IValueConverter<ICollection<long>, List<TimeSpan>>
{
    public List<TimeSpan> Convert(ICollection<long> sourceMember, ResolutionContext context)
    {
        if (sourceMember == null) return null;

        //return sourceMember.Select(t => TimeSpan.FromTicks(t)).ToList();
        return sourceMember.Select(t => TimeSpan.FromSeconds(t)).ToList();
    }

}