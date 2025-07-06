using CriptoControl.Model;
using CriptoControl.Model.DTO.CriptoMSG;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CriptoControl.Infrastructure.Service.Consumer
{
    public class ConsumerService : BackgroundService
    {
        private const string Queue = "Consumer MSG"; //Setting the queue name
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        // Initializing the RabbitMQ connection and declaring the queue
        public ConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                // If publishing externally, you may need to set credentials "password" via IConfiguration
            };

            // Establish a connection and open a channel
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declare the queue to ensure it exists (no durability, not exclusive, won't auto-delete)
            _channel.QueueDeclare(
                queue: Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        // This method runs when the service starts and sets up the message consumer
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            // Define what happens when a message is received
            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var InfoJson = Encoding.UTF8.GetString(byteArray);

                var consumerInfo = JsonSerializer.Deserialize<Cripto>(InfoJson);
                // At this point we have the message object and could trigger business logic

                // Create a new message to be published based on the received data
                var criptoMsg = new CriptoMSGIntegrationEvent(consumerInfo.Name);
                var criptoMsgJson = JsonSerializer.Serialize(criptoMsg);
                var criptoMsgBytes = Encoding.UTF8.GetBytes(criptoMsgJson);

                // Publish the new message to another queue with a dynamic routing key
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: "Message Cripto Received" + consumerInfo.Name,
                    basicProperties: null,
                    body: criptoMsgBytes);

                // Acknowledge that the message has been successfully processed
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}