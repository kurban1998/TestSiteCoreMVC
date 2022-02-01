using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;



namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Pen> PenRepository { get; }

        public UnitOfWork(MyDbContext databaseContext, IGenericRepository<Pen> penRepository)
        {
            _databaseContext = databaseContext;
            PenRepository = penRepository;
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        private readonly MyDbContext _databaseContext;
    }
}
