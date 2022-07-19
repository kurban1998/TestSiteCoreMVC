using AutoFixture;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using ManagementApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDataAccessLayer.Interfaces;
using MyDataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace ManagementApi.UnitTests
{
    [TestClass]
    public class ManagementApiControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IPenBuilder> _penBuilder;
        private ManagementApiController _target;
        private Mock<IGenericRepository<Pen>> _penRepository;
        private Mock<IGenericRepository<Brand>> _brandRepository;
        private readonly Fixture _fixture = new Fixture();
        [TestInitialize]
        public void TestInitialize() 
        {
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _penBuilder = new Mock<IPenBuilder>(MockBehavior.Strict);
            _target = new ManagementApiController(_unitOfWork.Object, _penBuilder.Object);
            _penRepository = new Mock<IGenericRepository<Pen>>(MockBehavior.Strict);
            _brandRepository = new Mock<IGenericRepository<Brand>>(MockBehavior.Strict);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public void ManagementApiController_PostTodoItem_Success()
        {
            // Arrange
            var pen = _fixture.Create<Pen>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.SetupGet(y => y.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Setup(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.Setup(x => x.Save());
            _penBuilder.Setup(z => z.Create()).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetBrand(It.IsAny<Brand>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetColor(It.IsAny<string>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetPrice(It.IsAny<double>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.Build()).Returns(pen);
            // Act
            _target.PostTodoItem(pen);
            // Assert
            _unitOfWork.Verify(x => x.Save(), Times.Once);
            _unitOfWork.Verify(x => x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Verify(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.Once);
            _unitOfWork.VerifyGet(y => y.BrandRepository, Times.Once);
            _penBuilder.Verify(z => z.Create(), Times.Once);
            _penBuilder.Verify(z => z.SetBrand(It.IsAny<Brand>()), Times.Once);
            _penBuilder.Verify(z => z.SetColor(It.IsAny<string>()), Times.Once);
            _penBuilder.Verify(z => z.SetPrice(It.IsAny<double>()), Times.Once);
            _penBuilder.Verify(z => z.Build(), Times.Once);
        }
        [TestMethod]
        public void ManagementApiController_GetTodoPens_Success()
        { 
            // Arrange
            var pens = _fixture.Create<List<Pen>>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetAll()).Returns(pens.AsQueryable);
            // Act
            _target.GetTodoPens();
            // Assert
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.PenRepository.GetAll(), Times.Once);
        }
        [TestMethod]
        public void ManagementApiController_GetTodoBrands_Success()
        {
            // Arrange
            var brands = _fixture.Create<List<Brand>>();
            _unitOfWork.SetupGet(x => x.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.BrandRepository.GetAll()).Returns(brands.AsQueryable);
            // Act
            _target.GetTodoBrands();
            // Assert
            _unitOfWork.VerifyGet(x => x.BrandRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.BrandRepository.GetAll(), Times.Once);
        }
        [TestMethod]
        public void ManagementApiController_GetTodoItem_Success()
        {
            // Arrange
            var penId = _fixture.Create<int>();
            var pen = _fixture.Create<Pen>();
            _unitOfWork.SetupGet(x => x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.GetById(penId)).Returns(pen);
            // Act
            _target.GetTodoItem(penId);
            // Assert
            _unitOfWork.VerifyGet(x => x.PenRepository, Times.AtLeastOnce);
            _unitOfWork.Verify(x => x.PenRepository.GetById(penId), Times.Once);
        }
        [TestMethod]
        public void ManagementApiController_DeleteTodoItem_Success()
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
            _target.DeleteTodoItem(penId);
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
