using Microsoft.AspNetCore.Mvc;
using MyDataAccessLayer.Models;
using MyWebAppProject.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using DataAccessLayer.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using MyDataAccessLayer.Interfaces;
using ManagementApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Policy;

namespace MyWebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IPenBuilder _penBuilder;
        private readonly ManagementApiOptions _apiOptions;
        public HomeController( HttpClient httpclient, IPenBuilder penBuilder,
            IOptions<ManagementApiOptions> apiOptions)
        {
            _httpClient = httpclient;
            _penBuilder = penBuilder;
            _apiOptions = apiOptions.Value;
        }
        public async Task<IActionResult> Index()
        {
            var urlForPens = _apiOptions.ManagementApiUrl;
            var urlForBrand = _apiOptions.ManagementApiUrl.Insert(urlForPens.Length,"/brands");   

            var httpResponseMessage1 =
                await _httpClient.GetStringAsync(urlForPens);
            var httpResponseMessage2 =
                await _httpClient.GetStringAsync(urlForBrand);

            var jsonPens = httpResponseMessage1;
            var jsonBrands = httpResponseMessage2;

            List<Pen> pens = JsonConvert.DeserializeObject<List<Pen>>(jsonPens);
            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(jsonBrands);
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
            var url = _apiOptions.ManagementApiUrl;
            var pen = _penBuilder
               .Create()
               .SetBrand(new Brand(brand))
               .SetColor(color)
               .SetPrice(price)
               .Build();

            var todoItemJson = new StringContent(
              JsonConvert.SerializeObject(pen),
              Encoding.UTF8,
              Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync(url, todoItemJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteFromDataBase(int id)
        {
            var penId = $"/{id}";
            var url = _apiOptions.ManagementApiUrl;
            using var httpResponseMessage =
                await _httpClient.DeleteAsync(url.Insert(url.Length,penId));
            
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
