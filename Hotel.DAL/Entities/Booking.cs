using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int GuestId { get; set; }
        
        [ForeignKey("GuestId")]
        //[JsonIgnore]
        public virtual Guest Guest { get; set; }

        public int RoomId { get; set; }
        
        [ForeignKey("RoomId")]
        //[JsonIgnore]
        public virtual Room Room { get; set; }

        public DateTime? BookingDate { get; set; }

        public DateTime EnterDate { get; set; }

        public DateTime LeaveDate { get; set; }

        public bool IsGuestSettledIn { get; set; }
    }
}