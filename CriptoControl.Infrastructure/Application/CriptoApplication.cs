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

        public void ProcessCreate(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " Created", criptoBytes);
        }
        
        public void ProcessGetOne(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " Obtained", criptoBytes);
        }

        public void ProcessUpdate(Cripto criptoCreate)
        {
            var criptoJson = JsonSerializer.Serialize(criptoCreate);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto " + criptoCreate.Name + " updated", criptoBytes);
        }

        public void ProcessDelete(int id)
        {
            var criptoJson = JsonSerializer.Serialize(id);

            var criptoBytes = Encoding.UTF8.GetBytes(criptoJson);

            _messageBusService.Publish("Cripto Id = " + id + " delete", criptoBytes);
        }
    }
}
