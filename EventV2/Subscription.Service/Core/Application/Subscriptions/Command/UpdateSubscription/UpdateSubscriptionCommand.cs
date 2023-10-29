using FluentValidation;
using MediatR;
using Subscription.Application.Interfaces;

namespace Subscription.Application.Subscriptions.Command.UpdateSubscription;

public class UpdateSubscriptionCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string? Email { get; set; }
    public ICollection<TimeSpan>? Dates { get; set; }
}

public class UpdateSubscriptionHandler : IRequestHandler<UpdateSubscriptionCommand>
{
    private readonly ISubscriptionRepository _repository;

    public UpdateSubscriptionHandler(ISubscriptionRepository repository) =>
        _repository = repository;

    public async Task<Unit> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateSubscriptions(request, cancellationToken);

        return Unit.Value;
    }
}

public class UpdateSubscriptionValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionValidator()
    {
        RuleFor(command =>
            command.EventId).NotEmpty();
    }
}