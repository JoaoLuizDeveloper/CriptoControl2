using CriptoControl.Model.Interfaces.IServices;
using RabbitMQ.Client;

namespace CriptoControl.Infrastructure.Service.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                // If publishing externally, you may need to set credentials "password" via IConfiguration
            };
        }

        // the method to publish messages to a specified queue
        public void Publish(string queue, byte[] message)
        {
            using(var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Declare the queue to ensure it exists (no durability, not exclusive, won't auto-delete)
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //Publish the message to the specified queue
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message);
                }
            }
        }
    }
}