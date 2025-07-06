using CriptoControl.Model;
using CriptoControl.Model.Interfaces.IApplication;
using CriptoControl.Model.Interfaces.IServices;
using System.Text;
using System.Text.Json;

namespace CriptoControl.Infrastructure.Application
{
    public class CriptoApplication : ICriptoApplication
    {
        private readonly IMessageBusService _messageBusService;
        //private const string queueName = "Cripto Created";
        public CriptoApplication(IMessageBusService messageBusService) 
        {
            _messageBusService = messageBusService;
        }

        // This method processes the creation of a Cripto
        // it serializes the Cripto object to JSON, converts it to bytes, and publishes it to the message bus
        public void ProcessCreate(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " Created", criptoBytes);
        }

        // This method processes the retrieval of a Cripto by its ID
        // it serializes the Cripto object to JSON, converts it to bytes, and publishes it to the message bus
        public void ProcessGetOne(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " Obtained", criptoBytes);
        }

        // This method processes the update of a Cripto
        // it serializes the Cripto object to JSON, converts it to bytes, and publishes it to the message bus
        public void ProcessUpdate(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " updated", criptoBytes);
        }

        // This method processes the deletion of a Cripto by its ID
        // it serializes the ID to JSON, converts it to bytes, and publishes it to the message bus
        public void ProcessDelete(int id)
        {
            var criptoJson = JsonSerializer.Serialize(id);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto Id = " + id + " delete", criptoBytes);
        }
    }
}
