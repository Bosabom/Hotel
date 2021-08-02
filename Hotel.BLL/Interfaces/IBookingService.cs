using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTO;

namespace Hotel.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetAllBookings();
        BookingDTO Get(int id);
        void Create(BookingDTO bookingDTO);

        double GetProfitForMonth(DateTime date);

        IEnumerable<RoomDTO> GetFreeRoomsOnPeriod(DateTime date_from, DateTime date_to);

        void Update(int id, BookingDTO booking_for_update);

        void Delete(int id);
    }
}
