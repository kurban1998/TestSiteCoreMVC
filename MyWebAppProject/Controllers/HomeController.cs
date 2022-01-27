using DataAccessLayer.DataBase;
using DataAccessLayer.Models;
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

        
        MyDbContext _db;
        GenericRepository<Pen> _gr;
        public HomeController(MyDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View(_db.Pens.ToList());
        }
        
        [HttpGet]
        public string AddToDataBase(string brand, string color, double price)
        {
            _gr = new GenericRepository<Pen>(_db);
            Pen pen = new Pen() { 
                Brand = brand,
                Color=color,
                Price=price
            };
            _gr.Add(pen);
            //_db.Pens.Add(pen);
            _db.SaveChanges();
            return "Успешно добавлена";
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
