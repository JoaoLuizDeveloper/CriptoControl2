namespace CriptoControl.Model.Interfaces.IApplication
{
    public interface ICriptoApplication
    {
        void ProcessCreate(Cripto criptoCreateDto);
        void ProcessGetOne(Cripto criptoCreate);
        void ProcessUpdate(Cripto criptoCreate);
        void ProcessDelete(int id);
    }
}