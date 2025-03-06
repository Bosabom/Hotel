using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public GuestDTO Guest { get; set; }

        public int RoomId { get; set; }

        public RoomDTO Room { get; set; }
        
        public DateTime? BookingDate { get; set; }

        public DateTime EnterDate { get; set; }

        public DateTime LeaveDate { get; set; }

        public bool IsGuestSettledIn { get; set; }
    }
}