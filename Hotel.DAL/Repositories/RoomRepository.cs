using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System.Data.Entity;

namespace Hotel.DAL.Repositories
{
    class RoomRepository : IRepository<Room>
    {
        private HotelModel db;
        public RoomRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            db.Rooms.Add(room);
        }
        
        public void Update(int id,Room updated_room)
        {
            var old_room = db.Rooms.Find(id);
            old_room.Active = updated_room.Active;

            db.Entry(old_room).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Room room = Get(id);
            db.Rooms.Remove(room);
        }
    }
}