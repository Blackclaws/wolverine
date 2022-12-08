using System;
using System.Threading;
using System.Threading.Tasks;
using JasperFx.Core;
using Wolverine.Transports.Sending;

namespace Wolverine.Runtime.Routing;

internal class NoNamedEndpointRoute : IMessageRoute
{
    private readonly string _message;

    public NoNamedEndpointRoute(string endpointName, string[] allNames)
    {
        EndpointName = endpointName;

        var nameList = allNames.Join(", ");
        _message = $"Endpoint name '{endpointName}' is invalid. Known endpoints are {nameList}";
    }

    public string EndpointName { get; }

    public Envelope CreateForSending(object message, DeliveryOptions? options, ISendingAgent localDurableQueue,
        WolverineRuntime runtime)
    {
        throw new UnknownEndpointException(_message);
    }

    public Task<T> InvokeAsync<T>(object message, MessagePublisher publisher,
        CancellationToken cancellation = default,
        TimeSpan? timeout = null) where T : class
    {
        throw new InvalidOperationException($"No endpoint with name '{EndpointName}'");
    }
}