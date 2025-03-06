using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using AutoMapper;

namespace Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IWorkUnit Database { get; set; }
        IMapper mapper;
        IMapper mapper_reverse;

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Room, RoomDTO>()).CreateMapper();

            mapper_reverse = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, Room>()).CreateMapper();
        }
        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            return mapper.Map<Room, RoomDTO>(Database.Rooms.Get(id));
        }
       
        public void Create(RoomDTO newRoom)
        {
            var allRooms = GetAllRooms();

            var data = allRooms.Where(c => c.Name == newRoom.Name
                        && c.CategoryId == newRoom.CategoryId).FirstOrDefault();

            if (data != null)
            {
                throw new Exception();
            }

            Database.Rooms.Create(mapper_reverse.Map<RoomDTO, Room>(newRoom));
            Database.Save();
        }

        public void Update(int id,RoomDTO updated_room)
        {
            var old_room = Database.Rooms.Get(id);
            if (old_room != null)
            {
                Database.Rooms.Update(id,mapper_reverse.Map<RoomDTO,Room>(updated_room));
                Database.Save();
            }
            else
                throw new Exception();
        }

        public void Delete(int id) 
        {
            var roomWithThisId = Database.Rooms.Get(id);
            if (roomWithThisId != null)
            {
                Database.Rooms.Delete(id);
                Database.Save();
            }
            else
                throw new Exception();
        }
    }
}