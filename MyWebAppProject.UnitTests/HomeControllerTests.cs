using AutoFixture;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebAppProject.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace MyWebAppProject.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private HomeController _target;
        private Mock<IGenericRepository<Pen>> _penRepository;
        private readonly Fixture _fixture = new Fixture();
        [TestInitialize]
        public void TestInitialize() 
        {
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _target = new HomeController(_unitOfWork.Object);
            _penRepository = new Mock<IGenericRepository<Pen>>(MockBehavior.Strict);
        }
        [TestMethod]
        public void HomeController_AddToDataBase_SaveSuccess()
        {
            // Arrange
            var brand = _fixture.Create<string>();
            var color = _fixture.Create<string>();
            var price = _fixture.Create<double>();
           _unitOfWork.SetupGet(x=>x.PenRepository).Returns(_penRepository.Object);
           _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
           _unitOfWork.Setup(x => x.Save());
            // Act
           _target.AddToDataBase(brand, color, price);
            // Assert
           _unitOfWork.Verify(x => x.Save(),Times.Once);
           _unitOfWork.Verify(x=>x.PenRepository.Add(It.IsAny<Pen>()));
           _unitOfWork.VerifyGet(x=>x.PenRepository,Times.Once);
        }
        [TestMethod]
        public void HomeController_Index_Success()
        { 
            // Arrange
            var brand = _fixture.Create<string>();
            var color = _fixture.Create<string>();
            var price = _fixture.Create<double>();
            List<Pen> pens = _fixture.Create<List<Pen>>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetAll()).Returns(pens.AsQueryable);
            _unitOfWork.Setup(x => x.Save());
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            // Act
            _target.Index();
            _target.AddToDataBase(brand, color, price);
            // Assert
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.PenRepository.GetAll(), Times.Once);
            _unitOfWork.Verify(x => x.PenRepository.Add(It.IsAny<Pen>()), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }
    }
}
