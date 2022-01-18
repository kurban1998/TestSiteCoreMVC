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
        private DbSet<T> _MyDbSet;
        public GenericRepository(MyDbContext context)
        {
            _databaseContext = context;
            _MyDbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            _MyDbSet.Add(item);
            _databaseContext.SaveChanges();
        }

        public void Delete(T item)
        {
            _MyDbSet.Remove(item);
            _databaseContext.SaveChanges();
        }

        IEnumerable<T> IGenericRepository<T>.GetAll()
        {
            return _MyDbSet.ToList<T>();
        }
    }
}
