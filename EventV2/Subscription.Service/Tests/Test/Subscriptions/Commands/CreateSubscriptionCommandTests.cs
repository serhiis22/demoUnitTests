using Subscription.Application.Subscriptions.Command.CreateSubscription;
using Subscription.Test.Common;

namespace Subscription.Test.Subscriptions.Commands;

public class CreateSubscriptionCommandTests : TestCommandBase
{

    [Fact]
    public async Task CreateSubscriptionHandler_Success()
    {
        // Arrange
        var command = new CreateSubscriptionCommand { EventId = Guid.Empty, Email = "test@example.com", Dates = new List<TimeSpan> { TimeSpan.FromHours(1) } };
        var handler = new CreateSubscriptionCommandHandler(Repository, Service);

        var sId = await handler.Handle(command, CancellationToken.None);
        // Assert
        var all = await Repository.GetSubscriptionsByEventId(Guid.Empty, CancellationToken.None);
        var res = all.Where(s =>
            s.Id == sId );
        Assert.NotNull(res);
    }
}