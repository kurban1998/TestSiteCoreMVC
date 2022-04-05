using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using MyDataAccessLayer.Models;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Pen> PenRepository { get; }
        public IGenericRepository<Brand> BrandRepository { get; }
        public UnitOfWork(
            MyDbContext databaseContext, 
            IGenericRepository<Pen> penRepository,
             IGenericRepository<Brand> brandRepository
            )
        {
            _databaseContext = databaseContext;
            PenRepository = penRepository;
            BrandRepository = brandRepository;
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }

        private readonly MyDbContext _databaseContext;
    }
}
