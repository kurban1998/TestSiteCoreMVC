using AutoFixture;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDataAccessLayer.Models;
using MyWebAppProject.Controllers;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Controllers;

namespace MyWebAppProject.UnitTests
{
    [TestClass]
    public class TodoItemsControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private TodoItemsController _target;
        private Mock<IGenericRepository<Pen>> _penRepository;
        private Mock<IGenericRepository<Brand>> _brandRepository;
        private readonly Fixture _fixture = new Fixture();
        [TestInitialize]
        public void TestInitialize() 
        {
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _target = new TodoItemsController(_unitOfWork.Object);
            _penRepository = new Mock<IGenericRepository<Pen>>(MockBehavior.Strict);
            _brandRepository = new Mock<IGenericRepository<Brand>>(MockBehavior.Strict);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public void TodoItemsController_PostTodoItem_Success()
        {
            // Arrange
            var pen = _fixture.Create<Pen>();
            _unitOfWork.SetupGet(x=>x.PenRepository).Returns(_penRepository.Object);
            _unitOfWork.SetupGet(y => y.BrandRepository).Returns(_brandRepository.Object);
            _unitOfWork.Setup(x => x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Setup(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.Setup(x => x.Save());
            // Act
            _target.PostTodoItem(pen);
            // Assert
            _unitOfWork.Verify(x => x.Save(),Times.Once);
            _unitOfWork.Verify(x=>x.PenRepository.Add(It.IsAny<Pen>()));
            _unitOfWork.Verify(y => y.BrandRepository.Add(It.IsAny<Brand>()));
            _unitOfWork.VerifyGet(x=>x.PenRepository,Times.Once);
            _unitOfWork.VerifyGet(y => y.BrandRepository, Times.Once);
        }
        [TestMethod]
        public void TodoItemsController_GetTodoPens_Success()
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
        public void TodoItemsController_GetTodoBrands_Success()
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
        public void TodoItemsController_GetTodoItem_Success()
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
        public void TodoItemsController_DeleteTodoItem_Success()
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
