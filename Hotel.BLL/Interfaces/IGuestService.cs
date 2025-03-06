using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetAllGuests();

        GuestDTO Get(int id);

        void Create(GuestDTO guestDTO);

        void Delete(int id);
    }
}