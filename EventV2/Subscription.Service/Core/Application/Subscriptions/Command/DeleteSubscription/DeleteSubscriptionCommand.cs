using FluentValidation;
using MediatR;
using Subscription.Application.Interfaces;

namespace Subscription.Application.Subscriptions.Command.DeleteSubscription;

public class DeleteSubscriptionCommand : IRequest
{
    public Guid EventId { get; set; }
    public string? Email { get; set; }

}

public class DeleteSubscriptionHandler : IRequestHandler<DeleteSubscriptionCommand>
{
    private readonly ISubscriptionRepository _repository;
    private readonly ISubscriptionService _subscriptionService;

    public DeleteSubscriptionHandler(ISubscriptionRepository repository, ISubscriptionService subscriptionService)
    {
        _repository = repository;
        _subscriptionService = subscriptionService;
    }

    public async Task<Unit> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        await _repository.RemoveSubscriptions(request.EventId, cancellationToken);
        await _subscriptionService.RemoveNotifications(request.EventId);
        return Unit.Value;
    }
}

public class DeleteSubscriptionValidator : AbstractValidator<DeleteSubscriptionCommand>
{
    public DeleteSubscriptionValidator()
    {
        RuleFor(command =>
            command.EventId).NotEmpty();
    }
}