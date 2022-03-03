using DataAccessLayer.DataBase;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyDataAccessLayer.UnitTests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private Fixture _builder = new Fixture();
        private UnitOfWork _target;
        private Mock<GenericRepository<Pen>> _genericRepository;
        private Mock<MyDbContext> _myDb;
        private MyDbContext _db;
        private DbContextOptions<MyDbContext> _options;
        [TestInitialize]
        public void TestInitialize()
        {
            
            _options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(@"Server=WIN-JN6720IOM98;Database=OnlineStore;Integrated Security=SSPI").Options;
            _db = new MyDbContext(_options);
            _myDb = new Mock<MyDbContext>(_options);
            _genericRepository = new Mock<GenericRepository<Pen>>(_myDb.Object);
            _target = new UnitOfWork(_myDb.Object, _genericRepository.Object);
        }
        [TestMethod]
        public void UnitOfWork_Save_Success()
        {
           
        }
    }
}
