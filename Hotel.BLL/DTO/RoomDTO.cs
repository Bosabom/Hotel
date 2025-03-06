using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //if room still active
        public bool Active { get; set; }
       
        public int CategoryId { get; set; }

        public CategoryDTO RoomCategory { get; set; }
    }
}