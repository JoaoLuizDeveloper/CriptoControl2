namespace CriptoControl.Model.Interfaces.IServices
{
    // This interface defines the contract for a message bus service
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}