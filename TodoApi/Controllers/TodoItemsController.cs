using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDataAccessLayer.Builder;
using MyDataAccessLayer.Models;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private PenBuilder _penBuilder = new PenBuilder();
        public TodoItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/TodoItems
        [HttpGet]
        public  Task<List<Pen>> GetTodoPens()
        {
            return  _unitOfWork.PenRepository.GetAll().ToListAsync();
        }
        // GET: api/TodoItems/brands
        [HttpGet("brands")]
        public  Task<List<Brand>> GetTodoBrands()
        {
            return  _unitOfWork.BrandRepository.GetAll().ToListAsync();
        }
        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public Pen GetTodoItem(int id)
        {
            var pen =  _unitOfWork.PenRepository.GetById(id);
            return pen;
        }

        // POST: api/TodoItems
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
            //post -h Content-Type=application/json -c "{"brand":"AAA","color":"red","price":100}"
        }

        // DELETE: api/TodoItems/5
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
