using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Subscription.Application.Interfaces;
using Subscription.Application.Subscriptions.Command.CreateSubscription;

namespace Subscription.Test.Subscriptions.Commands;

public class CreateSubscriptionCommandValidatorTests_
{
    private readonly CreateSubscriptionCommandValidator _validator;

    public CreateSubscriptionCommandValidatorTests_()
    {
        _validator = new CreateSubscriptionCommandValidator();
    }

    [Fact]
    public void Should_Fail_When_EventId_Is_Empty()
    {
        // Arrange
        var command = new CreateSubscriptionCommand
            { EventId = Guid.Empty, Email = "test@example.com", Dates = new List<TimeSpan> { TimeSpan.FromHours(1) } };

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(c => c.EventId);
    }

    [Fact]
    public void Should_Fail_When_Email_Is_Invalid()
    {
        // Arrange
        var command = new CreateSubscriptionCommand
            { EventId = Guid.NewGuid(), Email = "invalid_email", Dates = new List<TimeSpan> { TimeSpan.FromHours(1) } };

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Should_Pass_When_Command_Is_Valid()
    {
        // Arrange
        var command = new CreateSubscriptionCommand
        {
            EventId = Guid.NewGuid(), Email = "test@example.com", Dates = new List<TimeSpan> { TimeSpan.FromHours(1) }
        };

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }
}

public class CreateSubscriptionCommandHandlerTests
{
    private readonly Mock<ISubscriptionRepository> _repository;
    private readonly Mock<ISubscriptionService> _service;
    private readonly CreateSubscriptionCommandHandler _handler;

    public CreateSubscriptionCommandHandlerTests()
    {
        _repository = new Mock<ISubscriptionRepository>();
        _service = new Mock<ISubscriptionService>();
        _handler = new CreateSubscriptionCommandHandler(_repository.Object, _service.Object);
    }

    [Fact]
    public async Task Should_Create_Subscription()
    {
        // Arrange
        var command = new CreateSubscriptionCommand
        {
            EventId = Guid.NewGuid(), Email = "test@example.com", Dates = new List<TimeSpan> { TimeSpan.FromHours(1) }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _repository.Verify(r => r.AddSubscriptions(It.IsAny<Domain.Subscription>(), CancellationToken.None),
            Times.Once());
    }
}