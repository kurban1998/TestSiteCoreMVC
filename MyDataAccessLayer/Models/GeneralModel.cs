using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataAccessLayer.Models
{
    public class GeneralModel
    {
        public IEnumerable<Pen> Pens { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}
