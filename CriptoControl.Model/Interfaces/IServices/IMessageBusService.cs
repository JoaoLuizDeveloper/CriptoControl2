namespace CriptoControl.Model.Interfaces.IServices
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}