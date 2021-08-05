using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;


namespace Hotel.DAL.Repositories
{
    class GuestRepository:IRepository<Guest>
    {
        private HotelModel db;
        public GuestRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Guest> GetAll()
        {
            return db.Guests;
        }

        public Guest Get(int id)
        {
            return db.Guests.Find(id);
        }

        public void Create(Guest guest)
        {
            db.Guests.Add(guest);
        }
        public void Update(int id, Guest guest) { }
        public void Delete(int id)
        {
            Guest guest = Get(id);
            if (guest != null)
                db.Guests.Remove(guest);
        }

    }

}
