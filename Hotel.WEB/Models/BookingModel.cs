using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models
{
    public class BookingModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Guest ID")]
        public int GuestId { get; set; }

        [Required]
        [DisplayName("Room ID")]
        public int RoomId { get; set; }

        [DisplayName("Booking Date")]

        public DateTime? BookingDate { get; set; }

        [Required]
        [DisplayName("Enter Date")]
        public DateTime EnterDate { get; set; }

        [Required]
        [DisplayName("Departure Date")]
        public DateTime LeaveDate { get; set; }

        [Required]
        [DisplayName("Settlement")]
        public bool IsGuestSettledIn { get; set; }
    }
}