using DataAccessLayer.DataBase;
using DataAccessLayer.Models;
using System.Linq;

namespace MyDataAccessLayer.DataBase
{
   public class SampleData
    {
        public static void Initialize(MyDbContext context)
        {
            if (!context.Pens.Any())
            {
                context.Pens.AddRange(
                    new Pen
                    {
                       
                        Color = "yellow",
                        Price = 100
                    },
                    new Pen
                    {
                       
                        Color = "red",
                        Price = 20
                    },
                    new Pen
                    {
                       
                        Color = "green",
                        Price = 60
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
