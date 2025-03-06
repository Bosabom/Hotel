using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface IUserRepository<T>
    {
        T Get(T item);

        void Create(T item);
    }
}