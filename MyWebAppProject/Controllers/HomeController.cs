using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

using Microsoft.AspNetCore.Mvc;

using MyWebAppProject.Models;

using System.Diagnostics;
using System.Linq;



namespace MyWebAppProject.Controllers
{
    public class HomeController : Controller
    {        
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var pens = _unitOfWork.PenRepository.GetAll();
            return View(pens.ToList());
        }
        
        [HttpGet]
        public string AddToDataBase(string brand, string color, double price)
        {
            var pen = new Pen()
            { 
                Brand = brand,
                Color=color,
                Price=price
            };

            _unitOfWork.PenRepository.Add(pen);
            _unitOfWork.Save();

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

        private readonly IUnitOfWork _unitOfWork;
    }
}
