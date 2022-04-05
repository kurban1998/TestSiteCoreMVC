using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using MyDataAccessLayer.Models;
namespace DataAccessLayer.DataBase
{
    public class MyDbContext : DbContext
    {
        public DbSet<Pen> Pens { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        {
           
        }
        
    }
}
