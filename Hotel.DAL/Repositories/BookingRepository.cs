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
    class BookingRepository : IRepository<Booking>
    {
        private HotelModel db;
        public BookingRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
        }
        public void Update(int id, Booking updated_booking) 
        {
            var old_booking = db.Bookings.Find(id);

            //изменить можно:заехал ли постоялец 
            //дату выезда (а вдруг он продлил,или выехал раньше)
            //дату заезда - может заехал позже если предворительное бронирование было
          
            //если новая дата вьезда была указана в body
            if(updated_booking.EnterDate != DateTime.MinValue)
            {
                old_booking.EnterDate = updated_booking.EnterDate;
            }
            //если новая дата выезда была указана в body
            if (updated_booking.LeaveDate != DateTime.MinValue)
            {
                old_booking.LeaveDate = updated_booking.LeaveDate;
            }
            
            old_booking.IsGuestSettledIn = updated_booking.IsGuestSettledIn;

            db.Entry(old_booking).State = EntityState.Modified;

        }
        public void Delete(int id)
        {
            Booking booking = Get(id);
            if (booking != null)
                db.Bookings.Remove(booking);
           
        }
    }
}
