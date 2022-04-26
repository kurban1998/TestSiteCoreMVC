using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        public GenericRepository(MyDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        private DbSet<T> _dbSet;
    }
}
