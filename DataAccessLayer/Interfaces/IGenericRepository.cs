using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAllPens();
        void Add(T item);
        void Delete(T item);
    }
}
