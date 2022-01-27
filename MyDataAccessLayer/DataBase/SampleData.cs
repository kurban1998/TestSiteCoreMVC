using DataAccessLayer.DataBase;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Brand = "fff",
                        Color = "yellow",
                        Price = 100
                    },
                    new Pen
                    {
                        Brand = "bbb",
                        Color = "red",
                        Price = 20
                    },
                    new Pen
                    {
                        Brand = "aaa",
                        Color = "green",
                        Price = 60
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
