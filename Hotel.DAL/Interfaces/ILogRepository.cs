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

        //IEnumerable<T> GetByDate(DateTime date);

        void Create(T item);
    }
}
