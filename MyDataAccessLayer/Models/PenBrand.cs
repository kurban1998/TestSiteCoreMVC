using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataAccessLayer.Models
{
    public class PenBrand
    {
        public int id { get; set; }
        public string BrandName { get; set; }
        public int PenId { get; set; }
        public Pen Pen { get; set; }
    }
}
