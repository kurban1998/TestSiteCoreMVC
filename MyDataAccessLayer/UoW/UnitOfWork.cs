﻿using DataAccessLayer.DataBase;
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
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private MyDbContext _databaseContext = new MyDbContext();
    //    public GenericRepository<Pen> _penRepository;

    //    public UnitOfWork(MyDbContext databaseContext, GenericRepository<Pen> penRepository)
    //    {
    //        _databaseContext = databaseContext;
    //        _penRepository = penRepository;
    //    }

    //    public GenericRepository<Pen> Pens
    //    {
    //        get
    //        {
    //            if (_penRepository == null)
    //                _penRepository = new GenericRepository<Pen>(_databaseContext);
    //            return _penRepository;
    //        }
    //    }

    //    public void Save()
    //    {
    //        _databaseContext.SaveChanges();
    //    }
    //}
}
