using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MyDataAccessLayer.Models;
using MyWebAppProject.Models;
using System.Diagnostics;



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
            return View(pens);
        }
        
        [HttpGet]
        public string AddToDataBase(string brand, string color, double price)
        {
            var penBrand = new PenBrand()
            {
                BrandName = brand
            };
            var pen = new Pen()
            { 
                Brand = brand,
                Color=color,
                Price=price,
                PenBrand = penBrand
            };
            
            _unitOfWork.PenRepository.Add(pen);
            _unitOfWork.Save();

            return "Успешно добавлена";
        }
        public string DeleteFromDataBase(int id)
        {
            var pens = _unitOfWork.PenRepository.GetAll();
            foreach (var pen in pens)
            {
                if (pen.PenId == id)
                {
                    _unitOfWork.PenRepository.Delete(pen);
                }
            }
            _unitOfWork.Save();
            return "Ручка удалена из базы";
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
