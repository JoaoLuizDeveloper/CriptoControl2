namespace CriptoControl.Model.DTO.CriptoMSG
{
    public class CriptoMSGIntegrationEvent
    {
        public CriptoMSGIntegrationEvent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}