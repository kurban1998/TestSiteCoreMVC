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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pen>()
                .HasOne<Brand>(s => s.Brand)
                .WithMany(g => g.Pens)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
