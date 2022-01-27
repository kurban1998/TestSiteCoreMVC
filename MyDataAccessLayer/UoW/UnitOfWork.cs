using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _databaseContext;
        public GenericRepository<Pen> _penRepository;

        public UnitOfWork(MyDbContext databaseContext, GenericRepository<Pen> penRepository)
        {
            _databaseContext = databaseContext;
            _penRepository = penRepository;
        }

        public GenericRepository<Pen> Pens
        {
            get
            {
                if (_penRepository == null)
                    _penRepository = new GenericRepository<Pen>(_databaseContext);
                return _penRepository;
            }
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
