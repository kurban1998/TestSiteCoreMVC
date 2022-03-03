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
        private Fixture _builder = new Fixture();
        private string _brand => _builder.Create<string>();
        private string _color => _builder.Create<string>();
        private double _price => _builder.Create<double>();
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
            //Arrange
           _unitOfWork.SetupGet(x=>x.PenRepository).Returns(_penRepository.Object);
           _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
           _unitOfWork.Setup(x => x.Save());
            //Act
           _target.AddToDataBase(_brand, _color, _price);
            //Assert
           _unitOfWork.Verify(x => x.Save(),Times.Once);
           _unitOfWork.VerifyGet(x=>x.PenRepository,Times.Once);
        }
        [TestMethod]
        public void HomeController_AddToDataBase_AddSuccess()
        {
            //Arrange
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.Setup(x => x.Save());
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            //Act
            _target.AddToDataBase(_brand, _color, _price);
            //Assert
            _unitOfWork.Verify(x => x.PenRepository.Add(It.IsAny<Pen>()), Times.Once);
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.Once);
        }
        [TestMethod]
        public void HomeController_Index_Success()
        {
            //Arrange
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetAll()).Returns(new List<Pen>().AsQueryable);
            //Act
            _target.Index();
            //Assert
            _unitOfWork.Verify(x => x.PenRepository.GetAll(), Times.Once);
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.Once);
        }
    }
}
