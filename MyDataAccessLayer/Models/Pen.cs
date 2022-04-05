using MyDataAccessLayer.Models;

namespace DataAccessLayer.Models
{
    public class Pen
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
