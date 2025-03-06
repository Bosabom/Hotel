using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();

        RoomDTO Get(int id);

        void Create(RoomDTO roomDTO);

        void Update(int id, RoomDTO room_for_update);

        void Delete(int id);
    }
}