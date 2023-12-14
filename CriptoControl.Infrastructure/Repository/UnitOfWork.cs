using CriptoControl.Model.Interfaces.IRepository;

namespace CriptoControl.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Cripto = new CriptoRepository(_db);
        }

        public ICriptoRepository Cripto { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}