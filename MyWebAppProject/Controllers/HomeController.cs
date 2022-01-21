using DataAccessLayer.DataBase;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebAppProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        PenViewModel _pen = new PenViewModel();
        MyDbContext _db = new MyDbContext();
        GenericRepository<PenViewModel> _gr;
        UnitOfWork _uf;
        [HttpPost]
        public string AddToDataBase(string brand, string color, double price)
        {
            _pen.Brand = brand;
            _pen.Color = color;
            _pen.Price = price;

            _gr = new GenericRepository<PenViewModel>(_db);
            _gr.Add(_pen);
            _db.SaveChanges();

            return $"Ручка добавлена в базу";
        }
   
        public IActionResult Index()
        {
            return View();
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
