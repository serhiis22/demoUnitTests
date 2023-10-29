using AutoMapper;
using Subscription.Application.Common.Mapping;
using Subscription.Application.Subscriptions.Command.CreateSubscription;

namespace Subscription.WebApi.Models;

public class CreateSubscriptionDto : IMapWith<CreateSubscriptionCommand>
{
    public Guid EventId { get; set; }
    public string? Email { get; set; }
    public ICollection<long>? Dates { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSubscriptionDto, CreateSubscriptionCommand>()
            .ForMember(dest => dest.Dates , opt => opt.MapFrom(src => new LongToTimeSpanListConverter().Convert(src.Dates, null)));
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