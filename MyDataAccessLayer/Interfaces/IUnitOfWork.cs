using DataAccessLayer.Models;
using MyDataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepository<Pen> PenRepository { get; }
        IGenericRepository<Brand> BrandRepository { get; }
        void Save();
    }
}
