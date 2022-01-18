using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBase
{
    public class MyDbContext : DbContext
    {
        public DbSet<Pen> Pens { get; set; }
    }
}
