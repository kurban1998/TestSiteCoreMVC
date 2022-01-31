using DataAccessLayer.Models;



namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepository<Pen> PenRepository { get; }

        void Save();
    }
}
