using RabbitMQ.Client;
using SuperStore.Carts.Messages;
using SuperStore.Shared;

namespace SuperStore.Carts.Services
{
    internal sealed class MessagingBackgroundService : BackgroundService
    {
        private readonly IChannelFactory _channelFactory;
        private readonly IMessageSubscriber _messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger;

        public MessagingBackgroundService(IChannelFactory channelFactory,
            IMessageSubscriber messageSubscriber, ILogger<MessagingBackgroundService> logger)
        {
            _channelFactory = channelFactory;
            _messageSubscriber = messageSubscriber;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //declare exchange on the fly approach
            var channel = _channelFactory.Create();
            channel.ExchangeDeclare("Funds", "topic", false, false);

            _messageSubscriber
                .SubscribeMessage<FundsMessage>("carts-service-eu-many-words-queue", "EU.#", "Funds",
                (msg, args) =>
                {
                    _logger.LogInformation($"Received EU multiple-words message for customer: {msg.CustomerID} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");

                    return Task.CompletedTask;
                })
                .SubscribeMessage<FundsMessage>("carts-service-eu-single-word-queue", "EU.*", "Funds",
                (msg, args) =>
                {
                    _logger.LogInformation($"Received EU single-word message for customer: {msg.CustomerID} | Funds: {msg.CurrentFunds} | RoutingKey: {args.RoutingKey}");

                    return Task.CompletedTask;
                });
        }
    }
}
