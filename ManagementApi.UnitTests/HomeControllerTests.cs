using AutoFixture;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyDataAccessLayer.Interfaces;
using MyWebAppProject.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagementApi.UnitTests
{
    public class HomeControllerTests
    {
        private Mock<IPenBuilder> _penBuilder;
        private Mock<IOptions<ManagementApiOptions>> _apiOptions;
        private Mock<HttpClient> _httpClient;
        private HomeController _target;
        private readonly Fixture _fixture = new Fixture();
        [TestInitialize]
        public void TestInitialize()
        {
            _httpClient = new Mock<HttpClient>(MockBehavior.Strict);
            _penBuilder = new Mock<IPenBuilder>(MockBehavior.Strict);
            _target = new HomeController(_httpClient.Object, _penBuilder.Object, _apiOptions.Object);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [TestMethod]
        public void HomeControllerTest_Index_Success()
        { 
            //Arrange
            //Act
            //Assert
        }
        [TestMethod]
        public void HomeControllerTest_AddToDataBase_Success()
        {
            //Arrange
            
            //Act
            //Assert
        }
        [TestMethod]
        public void HomeControllerTest_DeleteFromDataBase_Success()
        {
            //Arrange
            var id = _fixture.Create<int>();
            var url = $"https://localhost:44391/api/ManagementApi/{id}";
            Task<HttpResponseMessage> task = new Task<HttpResponseMessage>();
            _httpClient.Setup(x => x.DeleteAsync(url)).Returns();
            _apiOptions.Setup(x => x.Value);
            //Act
            _target.DeleteFromDataBase(id);
            //Assert
        }
    }
}
