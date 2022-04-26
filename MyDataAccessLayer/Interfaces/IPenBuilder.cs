using DataAccessLayer.Models;
using MyDataAccessLayer.Models;

namespace MyDataAccessLayer.Interfaces
{
    public interface IPenBuilder
    {
        IPenBuilder Create(); 
        IPenBuilder SetColor(string color);
        IPenBuilder SetPrice(double price);
        IPenBuilder SetBrand(Brand brand);
        Pen Build();
    }
}
