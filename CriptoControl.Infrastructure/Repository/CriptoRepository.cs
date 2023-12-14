using CriptoControl.Model;
using CriptoControl.Model.Interfaces.IRepository;

namespace CriptoControl.Infrastructure.Repository
{
    public class CriptoRepository : RepositoryBase<Cripto>, ICriptoRepository
    {
        #region Construtor
        private readonly ApplicationDbContext _db;
        public CriptoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        #endregion
    }
}