using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface ILogRepository<T>
    {
        IEnumerable<T> GetAll();

        void Create(T item);

        //IEnumerable<T> GetByDate(DateTime date);
    }
}