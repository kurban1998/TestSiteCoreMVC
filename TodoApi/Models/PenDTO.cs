using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class PenDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public int BrandId { get; set; }
    }
}
