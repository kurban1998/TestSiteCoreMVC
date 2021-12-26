using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Pen
    {
        public int PenId { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
    }
}
