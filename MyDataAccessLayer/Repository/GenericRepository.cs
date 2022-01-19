using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        private MyDbContext _databaseContext;
        private DbSet<T> _dbSet;
        public GenericRepository(MyDbContext context)
        {
            _databaseContext = context;
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

        IEnumerable<T> IGenericRepository<T>.GetAll()
        {
            return _dbSet.ToList<T>();
        }
    }
}
