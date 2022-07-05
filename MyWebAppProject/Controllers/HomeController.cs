using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyDataAccessLayer.Models;
using MyWebAppProject.Models;
using System.Diagnostics;
using MyDataAccessLayer.Builder;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using DataAccessLayer.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Nancy.Json;

namespace MyWebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private PenBuilder _penBuilder = new PenBuilder();
        public HomeController(IUnitOfWork unitOfWork, HttpClient httpclient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpclient;
        }
        public async Task<IActionResult> Index()
        {
            var urlForPens = $"https://localhost:44391/api/todoitems";
            var urlForBrand = $"https://localhost:44391/api/todoitems/brands";

            var httpResponseMessage1 =
                await _httpClient.GetStringAsync(urlForPens);
            var httpResponseMessage2 =
                await _httpClient.GetStringAsync(urlForBrand);

            var jsonPens = httpResponseMessage1;
            var jsonBrands = httpResponseMessage2;

            List<Pen> pens = new JavaScriptSerializer().Deserialize<List<Pen>>(jsonPens);
            List<Brand> brands = new JavaScriptSerializer().Deserialize<List<Brand>>(jsonBrands);
            var gModel = new GeneralModel()
            {
                Pens = pens,
                Brands = brands,
            };

            return View(gModel);
        }

        [HttpPost]
        public async Task AddToDataBase(string brand, string color, double price)
        {
            var url = $"https://localhost:44391/api/todoitems";
            var pen = _penBuilder
               .Create()
               .SetBrand(new Brand(brand))
               .SetColor(color)
               .SetPrice(price)
               .Build();

            var todoItemJson = new StringContent(
              JsonSerializer.Serialize(pen),
              Encoding.UTF8,
              Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync(url, todoItemJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteFromDataBase(int id)
        {
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"https://localhost:44391/api/todoitems/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
