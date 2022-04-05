using DataAccessLayer.Models;
using System.Collections.Generic;

namespace MyDataAccessLayer.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pen> Pens { get; set; }
    }
}
