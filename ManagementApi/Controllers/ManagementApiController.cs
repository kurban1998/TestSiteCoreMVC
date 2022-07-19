using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDataAccessLayer.Models;
using MyDataAccessLayer.Interfaces;

namespace ManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPenBuilder _penBuilder;
        public ManagementApiController(IUnitOfWork unitOfWork, IPenBuilder penBuilder)
        {
            _unitOfWork = unitOfWork;
            _penBuilder = penBuilder;
        }

        // GET: api/ManagementApi
        [HttpGet]
        public  Task<List<Pen>> GetTodoPens()
        {
            return  _unitOfWork.PenRepository.GetAll().ToListAsync();
        }
        // GET: api/ManagementApi/brands
        [HttpGet("brands")]
        public  Task<List<Brand>> GetTodoBrands()
        {
            return  _unitOfWork.BrandRepository.GetAll().ToListAsync();
        }
        // GET: api/ManagementApi/5
        [HttpGet("{id}")]
        public Pen GetTodoItem(int id)
        {
            var pen =  _unitOfWork.PenRepository.GetById(id);
            return pen;
        }

        // POST: api/ManagementApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostTodoItem(Pen pen)
        {
            var newPen = _penBuilder
               .Create()
               .SetBrand(new Brand(pen.Brand.Name))
               .SetColor(pen.Color)
               .SetPrice(pen.Price)
               .Build();
            _unitOfWork.BrandRepository.Add(newPen.Brand);
            _unitOfWork.PenRepository.Add(newPen);
            _unitOfWork.Save();
        }

        // DELETE: api/ManagementApi/5
        [HttpDelete("{id}")]
        public void DeleteTodoItem(int id)
        {
            var pen = _unitOfWork.PenRepository.GetById(id);
            var brandId = pen.BrandId;
            var brand = _unitOfWork.BrandRepository.GetById(brandId);

            _unitOfWork.PenRepository.Delete(pen);
            _unitOfWork.BrandRepository.Delete(brand);
            _unitOfWork.Save();
        }
    }
}
