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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var urlForPens = _apiOptions.BaseUrl;
            var urlForBrand = _apiOptions.BaseUrl +"/brands";   

            var pensResponse =
                await _httpClient.GetStringAsync(urlForPens).ConfigureAwait(false);
            var brandsResponse =
                await _httpClient.GetStringAsync(urlForBrand).ConfigureAwait(false);

            var jsonPens = pensResponse;
            var jsonBrands = brandsResponse;

            var pens = JsonConvert.DeserializeObject<List<Pen>>(jsonPens);
            var brands = JsonConvert.DeserializeObject<List<Brand>>(jsonBrands);
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
            var url = _apiOptions.BaseUrl;
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
                await _httpClient.PostAsync(url, todoItemJson).ConfigureAwait(false);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteFromDataBase(int id)
        {
            var url = _apiOptions.BaseUrl;
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"{url}/{id}").ConfigureAwait(false);
            
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
