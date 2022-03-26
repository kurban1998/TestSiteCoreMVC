using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using MyDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBase
{
    public class MyDbContext : DbContext
    {
        public DbSet<Pen> Pens { get; set; }
        public DbSet<PenBrand> PenBrands { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
