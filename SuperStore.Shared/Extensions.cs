using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SuperStore.Shared.Connections;
using SuperStore.Shared.Publishers;
using SuperStore.Shared.Subscribers;

namespace SuperStore.Shared
{
    /// <summary>
    /// Registering everything which is related to the messaging by both microservicies
    /// </summary>
    public static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            var connection = factory.CreateConnection();

            //Q: Почему не AddTransient? или AddScoped?
            //A: Потому что для одного сервиса необходимо чтобы существовало одно подключение к RabbitMQ.
            // это где то в документации.. 
            services.AddSingleton(connection);
            services.AddSingleton<ChannelAccessor>();
            services.AddSingleton<IChannelFactory, ChannelFactory>();

            //
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();

            return services;
        }
    }
}
