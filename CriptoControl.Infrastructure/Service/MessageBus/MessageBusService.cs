using CriptoControl.Model.Interfaces.IServices;
using RabbitMQ.Client;

namespace CriptoControl.Infrastructure.Service.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        //private readonly IConfiguration _configuration;
        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                //Local não precisa configurar o password mas caso precise publicar sera necessario com o IConfiguration
                //Password
            };
        }

        public void Publish(string queue, byte[] message)
        {
            using(var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Garantir a criação da fila
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //Publicar a mensagem
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