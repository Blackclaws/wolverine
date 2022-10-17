using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;

namespace Wolverine.AmazonSqs.Tests;

public class bootstrapping
{
    [Fact]
    public async Task create_an_open_client()
    {
        using var host = await Host.CreateDefaultBuilder()
            .UseWolverine(opts =>
            {
                opts.UseAmazonSqsTransportLocally();
            }).StartAsync();

        var options = host.Services.GetRequiredService<WolverineOptions>();
        var transport = options.AmazonSqsTransport();
        
        // Just a smoke test on configuration here
        var queueNames = await transport.Client.ListQueuesAsync("wolverine");
    }

    [Fact]
    public async Task create_new_queue_on_startup()
    {
        var queueName = "wolverine-" + Guid.NewGuid().ToString();
        
        using var host = await Host.CreateDefaultBuilder()
            .UseWolverine(opts =>
            {
                opts.UseAmazonSqsTransportLocally().AutoProvision();

                opts.ListenToSqsQueue("wolverine-" + queueName);
            }).StartAsync();

        var options = host.Services.GetRequiredService<WolverineOptions>();
        var transport = options.AmazonSqsTransport();
        
        // Just a smoke test on configuration here
        var queueNames = await transport.Client.ListQueuesAsync("wolverine");
        var queueUrl = queueNames.QueueUrls.FirstOrDefault(x => x.Contains(queueName));
        queueUrl.ShouldNotBeNull();

        await transport.Client.DeleteQueueAsync(queueUrl);
    }
    
    [Fact]
    public async Task auto_purge_queue_on_startup_smoke_test()
    {
        var queueName = "wolverine-" + Guid.NewGuid().ToString();

        
        using var host = await Host.CreateDefaultBuilder()
            .UseWolverine(opts =>
            {
                opts.UseAmazonSqsTransportLocally().AutoPurgeOnStartup().AutoProvision();

                opts.ListenToSqsQueue("wolverine-" + queueName);
            }).StartAsync();

        var options = host.Services.GetRequiredService<WolverineOptions>();
        var transport = options.AmazonSqsTransport();

        var queueNames = await transport.Client.ListQueuesAsync("wolverine");
        var queueUrl = queueNames.QueueUrls.FirstOrDefault(x => x.Contains(queueName));
        queueUrl.ShouldNotBeNull();

        await transport.Client.DeleteQueueAsync(queueUrl);
    }
}