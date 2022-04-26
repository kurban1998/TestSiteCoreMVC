using AutoFixture;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDataAccessLayer.Models;
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
        private Mock<IGenericRepository<Brand>> _brandRepository;
        private readonly Fixture _fixture = new Fixture();
        [TestInitialize]
        public void TestInitialize() 
        {
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _target = new HomeController(_unitOfWork.Object);
            _penRepository = new Mock<IGenericRepository<Pen>>(MockBehavior.Strict);
            _brandRepository = new Mock<IGenericRepository<Brand>>(MockBehavior.Strict);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public void HomeController_AddToDataBase_Success()
        {
            // Arrange
            var brand = _fixture.Create<string>();
            var color = _fixture.Create<string>();
            var price = _fixture.Create<double>();
            _unitOfWork.SetupGet(x=>x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.SetupGet(y => y.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Setup(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.Setup(x => x.Save());
            // Act
            _target.AddToDataBase(brand, color, price);
            // Assert
            _unitOfWork.Verify(x => x.Save(),Times.Once);
            _unitOfWork.Verify(x=>x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Verify(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.VerifyGet(x=>x.PenRepository,Times.Once);
            _unitOfWork.VerifyGet(y => y.BrandRepository, Times.Once);
        }
        [TestMethod]
        public void HomeController_Index_Success()
        { 
            // Arrange
            var brand = _fixture.Create<string>();
            var color = _fixture.Create<string>();
            var price = _fixture.Create<double>();
            var pens = _fixture.Create<List<Pen>>();
            var brands = _fixture.Create<List<Brand>>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.SetupGet(y => y.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetAll()).Returns(pens.AsQueryable);
            _unitOfWork.Setup(y => y.BrandRepository.GetAll()).Returns(brands.AsQueryable);
            _unitOfWork.Setup(x => x.Save());
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Setup(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            // Act
            _target.Index();
            _target.AddToDataBase(brand, color, price);
            // Assert
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.PenRepository.GetAll(), Times.Once);
            _unitOfWork.Verify(x => x.PenRepository.Add(It.IsAny<Pen>()), Times.Once);
            _unitOfWork.VerifyGet(y => y.BrandRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(y => y.BrandRepository.GetAll(), Times.Once);
            _unitOfWork.Verify(y => y.BrandRepository.Add(It.IsAny<Brand>()), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }
        [TestMethod]
        public void HomeController_DeleteFromDataBase_Success()
        {
            // Arrange
            var penId = _fixture.Create<int>();
            var pen = _fixture.Create<Pen>();
            var brand = _fixture.Create<Brand>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.SetupGet(y => y.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetById(penId)).Returns(pen);
            var brandId = pen.BrandId;
            _unitOfWork.Setup(y => y.BrandRepository.GetById(brandId)).Returns(brand);
            _unitOfWork.Setup(x => x.PenRepository.Delete(pen));
            _unitOfWork.Setup(y => y.BrandRepository.Delete(brand));
            _unitOfWork.Setup(x => x.Save());
            //Act
            _target.DeleteFromDataBase(penId);
            //Assert
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.AtLeastOnce);
            _unitOfWork.VerifyGet(y => y.BrandRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.PenRepository.GetById(penId), Times.Once);
            _unitOfWork.Verify(y => y.BrandRepository.GetById(brandId), Times.Once);
            _unitOfWork.Verify(x => x.PenRepository.Delete(pen), Times.Once);
            _unitOfWork.Verify(y => y.BrandRepository.Delete(brand), Times.Once);
            _unitOfWork.Verify(x => x.Save(), Times.Once);
        }
    }
}
