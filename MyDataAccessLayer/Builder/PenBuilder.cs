using DataAccessLayer.Models;
using MyDataAccessLayer.Interfaces;
using MyDataAccessLayer.Models;

namespace MyDataAccessLayer.Builder
{
    public class PenBuilder : IPenBuilder
    {
        private Pen _pen;

        public IPenBuilder Create()
        {
            _pen = new Pen();
            return this;
        }
        public IPenBuilder SetBrand(Brand brand)
        {
            _pen.Brand = brand;
            return this;
        }

        public IPenBuilder SetColor(string color)
        {
            _pen.Color = color;
            return this;
        }

        public IPenBuilder SetPrice(double price)
        {
            _pen.Price = price;
            return this;
        }

        public Pen Build()
        {
            return _pen;
        }
    }
}
