using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataBase
{
    public class MyDbContext: DbContext
    {
        public DbSet<Pen> Pens { get; set; }
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new    DbInitializer());
        }
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }
    /// <summary>
    /// Инициализация базы данных
    /// </summary>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext db)
        {
            db.Pens.Add(new Pen { Brand = "Pilot", Price = 10, Color ="Blue"});
            db.Pens.Add(new Pen { Brand = "Erich Krause", Price = 100, Color = "Black" });
            db.Pens.Add(new Pen { Brand = "Pilot", Price = 15, Color = "Red" });
            db.SaveChanges();
        }
    }
}
