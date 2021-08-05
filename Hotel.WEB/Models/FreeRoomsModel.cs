using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models
{
    public class FreeRoomsModel
    {
        public string FirstDate { get; set; }

        public string SecondDate { get; set; }

        public List<RoomModel> FreeRooms { get; set; }
    }
}