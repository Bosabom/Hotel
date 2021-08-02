using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO Get(UserDTO userDTO);
        void Create(UserDTO userDTO);

    }
}
