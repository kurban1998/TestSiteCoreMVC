using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Interfaces;
using DataAccessLayer.DataBase;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace MyDataAccessLayer.UnitTests
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private Mock<MyDbContext> _mockContext;
        private GenericRepository<Pen> _target;
        private DbContextOptions<MyDbContext> _options;
        private Fixture _builder = new Fixture();
        private Pen _pen => _builder.Create<Pen>();
        
        [TestInitialize]
        public void TestInitialize()
        {

            _options = new DbContextOptionsBuilder<MyDbContext>()
               .UseSqlServer(@"Server=WIN-JN6720IOM98;Database=OnlineStore;Integrated Security=SSPI").Options;
            _mockContext = new Mock<MyDbContext>(_options);
            _target = new GenericRepository<Pen>(_mockContext.Object);
            
        }
        [TestMethod]
        public void GenericRepository_Add()
        {
            //         
            _mockContext.Setup(x => x.Add(_pen));
            //
            _target.Add(_pen);
            //
            _mockContext.Verify(x=>x.Add(_pen), Times.Once);    
        }
        public void GenericRepository_Delete()
        {
            //         
            _mockContext.Setup(x => x.Remove(_pen));
            //
            _target.Delete(_pen);
            //
            _mockContext.Verify(x => x.Remove(_pen), Times.Once);
        }
        public void GenericRepository_GetAll()
        {
          
        }
    }
}
