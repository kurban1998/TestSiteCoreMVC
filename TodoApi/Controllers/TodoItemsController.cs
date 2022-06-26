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
        public async Task<List<Pen>> GetTodoItems()
        {
            return await _unitOfWork.PenRepository.GetAll().ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public Pen GetTodoItem(int id)
        {
            var pen =  _unitOfWork.PenRepository.GetById(id);
            return pen;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public void PutTodoItem(int id, string brand, string color, double price)
        {
            var pen = _unitOfWork.PenRepository.GetById(id);
            pen.Brand.Name = brand;
            pen.Color = color;
            pen.Price = price;
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostTodoItem(string brand, string color, double price)
        {
            var pen = _penBuilder
                .Create()
                .SetBrand(new Brand("AAA"))
                .SetColor("red")
                .SetPrice(100)
                .Build();

            _unitOfWork.BrandRepository.Add(pen.Brand);
            _unitOfWork.PenRepository.Add(pen);
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
        private static PenDTO ItemToDTO(Pen pen) =>
            new PenDTO
            {
                Id = pen.Id,
                Price = pen.Price,
                Color = pen.Color,
                BrandId = pen.BrandId
            };
    }
}
