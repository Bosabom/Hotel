using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Newtonsoft.Json;
namespace Hotel.API.Models
{
    public class BookingModel
    {
        
        public int Id { get; set; }

       
        //public GuestModel Guest { get; set; }

        public int GuestId { get; set; }
     
       // public RoomModel Room { get; set; }
        public int RoomId { get; set; }

        public DateTime? BookingDate { get; set; }
        public DateTime EnterDate { get; set; }

        public DateTime LeaveDate { get; set; }
        public bool IsGuestSettledIn { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is BookingModel)
            {
                var objBM = obj as BookingModel;
                return this.Id == objBM.Id                  
                    && this.GuestId == objBM.GuestId
                    && this.RoomId == objBM.RoomId
                    && this.IsGuestSettledIn == objBM.IsGuestSettledIn
                    && this.BookingDate == objBM.BookingDate
                    && this.EnterDate == objBM.EnterDate
                    && this.LeaveDate == objBM.LeaveDate;
            }
            else
                return base.Equals(obj);
        }
    }
}