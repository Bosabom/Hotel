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
    public class BookingService : IBookingService
    {
        private IMapper bookingmapper_reverse;
        IMapper mapper;
        private IWorkUnit Database { get; set; }

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
            mapper= new MapperConfiguration(cfg =>
                 cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();

            bookingmapper_reverse = new MapperConfiguration(cfg =>
                 cfg.CreateMap<BookingDTO, Booking>()).CreateMapper();
        }
      
        public IEnumerable<BookingDTO> GetAllBookings()
        {
            return mapper.Map<IEnumerable<Booking>, List<BookingDTO>>(Database.Bookings.GetAll());
        }

        public BookingDTO Get(int id)
        {
            return mapper.Map<Booking, BookingDTO>(Database.Bookings.Get(id));
        }

        public double GetProfitForMonth(DateTime date)
        {
            var reservedRooms = Database.Bookings.GetAll()
                .Where(r => r.LeaveDate.Year == date.Year && r.LeaveDate.Month == date.Month);

            double sumProfit = 0;

            foreach (var room in reservedRooms)
            {
                int num_of_days = room.LeaveDate.Subtract(room.EnterDate).Days;
                sumProfit += Database.PriceCategories.GetAll()
                    .Where(pc => pc.CategoryId == room.Room.CategoryId && room.LeaveDate >= pc.StartDate && room.LeaveDate <= pc.EndDate)
                    .Select(p => p.Price).FirstOrDefault() * num_of_days;
            }
            return sumProfit;
        }

        public IEnumerable<RoomDTO> GetFreeRoomsOnPeriod(DateTime date_from, DateTime date_to)
        {
            var room_mapper = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Room, RoomDTO>()
               ).CreateMapper();

            var rooms_id_from_bookings = Database.Bookings.GetAll().Select(r => r.RoomId);

            var get_free_rooms_id_by_period = Database.Bookings.GetAll()
                .Where(res => (date_from > res.LeaveDate || date_to < res.EnterDate)).Select(r => r.RoomId);

            var free_rooms = Database.Rooms.GetAll()
                .Where(r => r.Active == true && 
                (get_free_rooms_id_by_period.Contains(r.Id) || !rooms_id_from_bookings.Contains(r.Id)));

            return room_mapper.Map<IEnumerable<Room>, List<RoomDTO>>(free_rooms);
        }

        public void Create(BookingDTO newBooking) 
        {
            var free_rooms_on_date = GetFreeRoomsOnPeriod(newBooking.EnterDate, newBooking.LeaveDate);
            var free_rooms_id=free_rooms_on_date.Select(r => r.Id);

            if (free_rooms_id.Contains(newBooking.RoomId))
            {
                Database.Bookings.Create(bookingmapper_reverse.Map<BookingDTO, Booking>(newBooking));
                Database.Save();
            }
            else
                throw new Exception();
        }

        public void Update(int id, BookingDTO booking_for_update)
        {
            var old_booking = Database.Bookings.Get(id);
            if (old_booking != null)
            {
                Database.Bookings.Update(id, bookingmapper_reverse.Map<BookingDTO, Booking>(booking_for_update));
                Database.Save();
            }
            else
                throw new Exception();
        }

        public void Delete(int id) 
        {
            var bookingWithThisId = Database.Bookings.Get(id);
            if (bookingWithThisId != null)
            {
                Database.Bookings.Delete(id);
                Database.Save();
            }
            else
                throw new Exception();
        }
    }
}