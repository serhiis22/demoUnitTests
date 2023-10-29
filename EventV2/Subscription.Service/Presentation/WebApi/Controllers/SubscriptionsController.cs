using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Subscription.Application.Subscriptions.Command.CreateSubscription;
using Subscription.Application.Subscriptions.Queries.GetSubscriptionList;
using Subscription.WebApi.Models;

namespace Subscription.WebApi.Controllers;

[Route("api/[controller]")]
public class SubscriptionsController : BaseController
{
    private readonly IMapper _mapper;

    public SubscriptionsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubscriptionListVm>> Get(Guid id)
    {
        var query = new GetSubscriptionListQuery
        {
            EventId = id
        };
        SubscriptionListVm vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateSubscriptionDto createEventDto)
    {
        var command = _mapper.Map<CreateSubscriptionCommand>(createEventDto);
        var eventId = await Mediator.Send(command);
        return Ok(eventId);
    }

}