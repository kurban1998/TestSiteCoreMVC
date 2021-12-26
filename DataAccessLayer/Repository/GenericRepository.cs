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
        private MyDbContext MyDb;
        private DbSet<T> MyDbSet;
        public GenericRepository(MyDbContext context)
        {
            this.MyDb = context;
            this.MyDbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            MyDbSet.Add(item);
            MyDb.SaveChanges();
        }

        public void Delete(T item)
        {
            MyDbSet.Remove(item);
            MyDb.SaveChanges();
        }

        IEnumerable<T> IGenericRepository<T>.GetAllPens()
        {
            return MyDbSet.ToList<T>();
        }
    }
}
