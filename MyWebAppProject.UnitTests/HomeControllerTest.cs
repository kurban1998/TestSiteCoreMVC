using AutoFixture;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebAppProject.Controllers;


namespace MyWebAppProject.UnitTests
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private HomeController _controller;
        private Mock<IGenericRepository<Pen>> _penRepositoryMock;
        [TestInitialize]
        public void TestInitialize() 
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new HomeController(_unitOfWorkMock.Object);
            _penRepositoryMock = new Mock<IGenericRepository<Pen>>();
        }
        [TestMethod]
        public void HomeController_AddToDataBase_SaveSuccess()
        {
           _unitOfWorkMock.SetupGet(x=>x.PenRepository).Returns(_penRepositoryMock.Object);
           _controller.AddToDataBase("1", "2", 520);
           _unitOfWorkMock.Verify(x => x.Save(),Times.Once);
            //проверка вызова метода Save в AddToDataBase
        }
        [TestMethod]
        public void HomeController_AddToDataBase_AddSuccess()
        {
            _unitOfWorkMock.SetupGet(x => x.PenRepository).Returns(_penRepositoryMock.Object);
            _controller.AddToDataBase("1", "2", 20);
            _unitOfWorkMock.Verify(x => x.PenRepository.Add(It.IsAny<Pen>()), Times.Once);
            //проверка вызова метода Add в AddToDataBase
        }
        [TestMethod]
        public void HomeController_Index_GetAllSuccess()
        {
            _unitOfWorkMock.SetupGet(x => x.PenRepository).Returns(_penRepositoryMock.Object);
            _controller.Index();
            _unitOfWorkMock.Verify(x => x.PenRepository.GetAll(), Times.Once);
            //проверка вызова метода GetAll в Index
        }
    }
}
