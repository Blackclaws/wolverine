using CoreTests.Persistence.Sagas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace CoreTests.Acceptance;

public class warn_if_using_wolverine_before_app_is_started 
{

    [Fact]
    public async Task will_throw_on_operations()
    {
        using var theHost = Host.CreateDefaultBuilder().UseWolverine().Build();
        
        var theBus = theHost.Services.GetRequiredService<IMessageBus>();

        await Should.ThrowAsync<WolverineHasNotStartedException>(async () => theBus.SendAsync(new Message1()));
        await Should.ThrowAsync<WolverineHasNotStartedException>(async () => theBus.PublishAsync(new Message1()));
        await Should.ThrowAsync<WolverineHasNotStartedException>(async () => theBus.InvokeAsync(new Message1()));

    }
}