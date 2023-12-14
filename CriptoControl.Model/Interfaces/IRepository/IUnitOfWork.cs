namespace CriptoControl.Model.Interfaces.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICriptoRepository Cripto { get; }
    }
}
