using FluentValidation;
using MediatR;
using Subscription.Application.Interfaces;
using Subscription.Domain;

namespace Subscription.Application.Subscriptions.Command.CreateSubscription;

public class CreateSubscriptionCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string? Email { get; set; }
    public ICollection<TimeSpan>? Dates { get; set; }
}

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ISubscriptionRepository _repository;
    private readonly ISubscriptionService _subscriptionService;
    
    public CreateSubscriptionCommandHandler(ISubscriptionRepository repository, ISubscriptionService subscriptionService)
    {
        _repository = repository;
        _subscriptionService = subscriptionService;
    }
    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var createSubscription = new Domain.Subscription
        {
            Id = Guid.NewGuid(),
            EventId = request.EventId,
            Email = request.Email,
            Dates = request.Dates.Select(date => new SubscriptionsDate { NotificationTime = date }).ToList()
        };
        await _repository.AddSubscriptions(createSubscription, cancellationToken);
        await _subscriptionService.CreateNotifications(createSubscription);
        return createSubscription.Id;
    }


}
public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(command =>
            command.EventId).NotEmpty();
        RuleFor(command =>
                command.Email).NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email is required");
    }
}