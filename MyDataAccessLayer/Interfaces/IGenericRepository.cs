using System.Linq;



namespace DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        void Add(T item);
        void Delete(T item);
        T GetById(int id);
    }
}
