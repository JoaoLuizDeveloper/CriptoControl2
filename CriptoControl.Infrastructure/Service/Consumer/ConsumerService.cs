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
        private const string Queue = "Consumer MSG";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                //Local não precisa configurar o password mas caso precise publicar sera necessario com o IConfiguration
                //Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var InfoJson = Encoding.UTF8.GetString(byteArray);

                var consumerInfo = JsonSerializer.Deserialize<Cripto>(InfoJson);
                // a partir daqui já tenho a informação da mensageria e poderia fazer algo com ela

                var criptoMsg = new CriptoMSGIntegrationEvent(consumerInfo.Name);
                var criptoMsgJson = JsonSerializer.Serialize(criptoMsg);
                var criptoMsgBytes = Encoding.UTF8.GetBytes(criptoMsgJson);

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: "Message Cripto Received" + consumerInfo.Name,
                    basicProperties: null,
                    body: criptoMsgBytes);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}