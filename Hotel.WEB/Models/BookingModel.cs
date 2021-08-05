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
        [DataType(DataType.Date)]
        public DateTime EnterDate { get; set; }
        [DisplayName("Enter Date")]
        public string EnterDate_short
        {
            get
            {
                return EnterDate.ToShortDateString();
            }
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LeaveDate { get; set; }

        [DisplayName("Departure Date")]
        public string LeaveDate_short
        {
            get
            {
                return LeaveDate.ToShortDateString();
            }
        }

        [Required]
        [DisplayName("Settlement")]
        
        public bool IsGuestSettledIn { get; set; }

        public override string ToString()
        {
            return $"Room ID = {RoomId}; Guest ID = {GuestId}; Booking Date = {BookingDate};" +
                $"Ener Date = {EnterDate}; Leave Date = {LeaveDate}; Settlement = {IsGuestSettledIn}";
        }
    }
}