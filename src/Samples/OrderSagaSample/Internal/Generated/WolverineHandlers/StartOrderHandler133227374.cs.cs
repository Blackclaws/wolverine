// <auto-generated/>
#pragma warning disable
using Microsoft.Extensions.Logging;
using OrderSagaSample;
using Wolverine.Marten.Publishing;
using Wolverine.Runtime;
using Wolverine.Runtime.Handlers;

namespace Internal.Generated.WolverineHandlers
{
    #region sample_generated_code_for_start_order_handler

    public class StartOrderHandler133227374 : MessageHandler
    {
        private readonly OutboxedSessionFactory _outboxedSessionFactory;
        private readonly ILogger<Order> _logger;

        public StartOrderHandler133227374(OutboxedSessionFactory outboxedSessionFactory, ILogger<Order> logger)
        {
            _outboxedSessionFactory = outboxedSessionFactory;
            _logger = logger;
        }

        public override async Task HandleAsync(MessageContext context, CancellationToken cancellation)
        {
            var startOrder = (StartOrder)context.Envelope.Message;
            await using var documentSession = _outboxedSessionFactory.OpenSession(context);
            (var outgoing1, var outgoing2) = Order.Start(startOrder, _logger);
            
            // Register the document operation with the current session
            documentSession.Insert(outgoing1);
            
            // Outgoing, cascaded message
            await context.EnqueueCascadingAsync(outgoing2).ConfigureAwait(false);
            
            // Commit the unit of work
            await documentSession.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }
    }

    #endregion

}
