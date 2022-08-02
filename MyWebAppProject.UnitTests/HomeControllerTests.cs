using AutoFixture;
using DataAccessLayer.Models;
using ManagementApi;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Contrib.HttpClient;
using MyDataAccessLayer.Interfaces;
using MyDataAccessLayer.Models;
using MyWebAppProject.Controllers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebAppProject.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IPenBuilder> _penBuilder;
        private Mock<IOptions<ManagementApiOptions>> _apiOptions;
        private HttpClient _httpClient;
        private HomeController _target;
        private readonly Fixture _fixture = new Fixture();
        private Mock<HttpMessageHandler> _handler;
        [TestInitialize]
        public void TestInitialize()
        {
            _apiOptions = new Mock<IOptions<ManagementApiOptions>>();
            _apiOptions.SetupGet(x => x.Value).Returns(new ManagementApiOptions { BaseUrl = _fixture.Create<Uri>().AbsoluteUri });
            _handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = _handler.CreateClient();
            _penBuilder = new Mock<IPenBuilder>(MockBehavior.Strict);
            _target = new HomeController(_httpClient, _penBuilder.Object, _apiOptions.Object);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public async Task HomeController_Index_Success()
        {
            //Arrange
            var pen = _fixture.Create<Pen>();
            _handler.SetupRequest(HttpMethod.Get, $"{_apiOptions.Object.Value.BaseUrl}")
                .ReturnsResponse(HttpStatusCode.OK);
            _handler.SetupRequest(HttpMethod.Get, $"{_apiOptions.Object.Value.BaseUrl}/brands")
                .ReturnsResponse(HttpStatusCode.OK);
            //Act
            await _target.Index();
            //Assert
            _handler.VerifyRequest(HttpMethod.Get, $"{_apiOptions.Object.Value.BaseUrl}");
            _handler.VerifyRequest(HttpMethod.Get, $"{_apiOptions.Object.Value.BaseUrl}/brands");
        }
        [TestMethod]
        public async Task HomeController_AddToDataBase_SuccessAsync()
        {
            //Arrange
            var brand = _fixture.Create<string>();
            var color = _fixture.Create<string>();
            var price = _fixture.Create<double>();
            var pen = _fixture.Create<Pen>();
            _handler.SetupRequest(HttpMethod.Post, $"{_apiOptions.Object.Value.BaseUrl}")
                .ReturnsResponse(HttpStatusCode.OK);
            _penBuilder.Setup(z => z.Create()).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetBrand(It.IsAny<Brand>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetColor(It.IsAny<string>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.SetPrice(It.IsAny<double>())).Returns(_penBuilder.Object);
            _penBuilder.Setup(z => z.Build()).Returns(pen);
            //Act
            await _target.AddToDataBase(brand, color, price);
            //Assert
            _handler.VerifyRequest(HttpMethod.Post, $"{_apiOptions.Object.Value.BaseUrl}");
            _penBuilder.Verify(z => z.Create(), Times.Once);
            _penBuilder.Verify(z => z.SetBrand(It.IsAny<Brand>()), Times.Once);
            _penBuilder.Verify(z => z.SetColor(It.IsAny<string>()), Times.Once);
            _penBuilder.Verify(z => z.SetPrice(It.IsAny<double>()), Times.Once);
            _penBuilder.Verify(z => z.Build(), Times.Once);
        }
        [TestMethod]
        public async Task HomeController_DeleteFromDataBase_Success()
        {
            //Arrange
            var pen = _fixture.Create<Pen>();
            _handler.SetupRequest(HttpMethod.Delete, $"{_apiOptions.Object.Value.BaseUrl}/{pen.Id}")
                .ReturnsResponse(HttpStatusCode.OK);
            //Act
            await _target.DeleteFromDataBase(pen.Id);
            //Assert
            _handler.VerifyRequest(HttpMethod.Delete, $"{_apiOptions.Object.Value.BaseUrl}/{pen.Id}");
        }
    }
}
