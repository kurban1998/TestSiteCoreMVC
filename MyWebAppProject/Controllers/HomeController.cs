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
            var gModel = new GeneralModel()
            {
                Pens = _unitOfWork.PenRepository.GetAll(),
                Brands = _unitOfWork.BrandRepository.GetAll(),
            };
             
            return View(gModel);
        }
        
        [HttpGet]
        public string AddToDataBase(string brand, string color, double price)
        {

            var penBrand = new Brand()
            {
                Name = brand,
            };
            _unitOfWork.BrandRepository.Add(penBrand);
           var pen = new Pen (){
                Color = color,
                Price = price,
                Brand = penBrand,
            };
            
            _unitOfWork.PenRepository.Add(pen);
            _unitOfWork.Save();

            return "Успешно добавлена";
        }
        public string DeleteFromDataBase(int id)
        {
            var pen = _unitOfWork.PenRepository.GetById(id);
            var brandId = pen.BrandId;
            var brand = _unitOfWork.BrandRepository.GetById(brandId);
            
            _unitOfWork.PenRepository.Delete(pen);
            _unitOfWork.BrandRepository.Delete(brand);
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
