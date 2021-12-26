using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IDisposable
    {
        private MyDbContext db = new MyDbContext();
        private GenericRepository<Pen> penRepository;
        public GenericRepository<Pen> Pens
        {
            get
            {
                if (penRepository == null)
                    penRepository = new GenericRepository<Pen>(db);
                return penRepository;
            }
        }
       
        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
